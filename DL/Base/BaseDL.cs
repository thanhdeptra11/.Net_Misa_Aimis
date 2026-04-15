using Common.Model;
using Dapper;
using DL.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DL.Base
{
    public abstract class BaseDL<T> : IBaseDL<T> where T : class
    {
        protected readonly IDbConnectionFactory _connectionFactory;
        protected abstract string TableName { get; }

        protected virtual string IdColumnName => "id";
        protected virtual string IdPropertyName => "Id";

        // Mảng chứa các cột dùng để query LIKE khi có SearchTerm (cần overide ở class con)
        protected virtual string[] SearchColumns => Array.Empty<string>();


        // Cache tĩnh từng kiểu T

        private static readonly string _selectColumns;
        private static readonly string _insertColumns;
        private static readonly string _insertParameters;
        private static readonly IEnumerable<PropertyInfo> _validProperties;

        static BaseDL()
        {
            // Lọc ra các property có thể đọc và không có attribute [NotMapped]
            _validProperties = typeof(T).GetProperties()
                .Where(p => p.CanRead && p.GetCustomAttribute<NotMappedAttribute>() == null)
                .ToArray();

            // Cache sẵn chuỗi: "customer_id AS Id, customer_name AS CustomerName, ..."
            _selectColumns = string.Join(", ", _validProperties.Select(p =>
                $"{GetColumnNameStatic(p)} AS {p.Name}"
            ));

            // Cache sẵn chuỗi: "customer_id, customer_name, ..."
            _insertColumns = string.Join(", ", _validProperties.Select(p => GetColumnNameStatic(p)));

            // Cache sẵn chuỗi: "@Id, @CustomerName, ..."
            _insertParameters = string.Join(", ", _validProperties.Select(p => $"@{p.Name}"));
        }

        protected static string GetColumnNameStatic(PropertyInfo property)
        {
            var columnAttr = property.GetCustomAttribute<ColumnAttribute>(inherit: true);
            return columnAttr?.Name ?? property.Name;
        }


        public BaseDL(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            string query = $"SELECT {_selectColumns} FROM {TableName}";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<T>(query);
        }

        public virtual async Task<PagingResponse<T>> GetPagingAsync(PagingRequest request)
        {
            var searchCondition = "";
            var parameters = new DynamicParameters();
            
            if (!string.IsNullOrWhiteSpace(request.SearchTerm) && SearchColumns.Length > 0)
            {
                var conditions = SearchColumns.Select(c => $"{c} LIKE @SearchTerm");
                searchCondition = " WHERE " + string.Join(" OR ", conditions);
                parameters.Add("SearchTerm", $"%{request.SearchTerm}%");
            }

            var countQuery = $"SELECT COUNT(1) FROM {TableName} {searchCondition}";
            
            parameters.Add("Limit", request.PageSize);
            parameters.Add("Offset", (request.PageNumber - 1) * request.PageSize);
            
            var dataQuery = $"SELECT {_selectColumns} FROM {TableName} {searchCondition} ORDER BY {IdColumnName} DESC LIMIT @Limit OFFSET @Offset";

            using var connection = _connectionFactory.CreateConnection();
            
            var totalRecords = await connection.ExecuteScalarAsync<long>(countQuery, parameters);
            var data = await connection.QueryAsync<T>(dataQuery, parameters);
            
            var totalPages = totalRecords > 0 ? (int)Math.Ceiling(totalRecords / (double)request.PageSize) : 0;

            return new PagingResponse<T>(totalRecords, totalPages, data);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            string query = $"SELECT {_selectColumns} FROM {TableName} WHERE {IdColumnName} = @Id";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public virtual async Task<int> AddAsync(T entity)
        {
            string query = $"INSERT INTO {TableName} ({_insertColumns}) VALUES ({_insertParameters})";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(query, entity);
        }

        public virtual async Task<int> UpdateAsync(T entity)
        {
            // Update linh hoạt, loại bỏ property khóa chính động thay vì hardcode
            var updateProperties = _validProperties.Where(p => p.Name != IdPropertyName);
            var updates = string.Join(", ", updateProperties.Select(p => $"{GetColumnNameStatic(p)} = @{p.Name}"));

            string query = $"UPDATE {TableName} SET {updates} WHERE {IdColumnName} = @{IdPropertyName}";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(query, entity);
        }

        public virtual async Task<int> DeleteAsync(Guid id)
        {
            string query = $"DELETE FROM {TableName} WHERE {IdColumnName} = @Id";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(query, new { Id = id });
        }

        public virtual async Task<int> DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            string query = $"DELETE FROM {TableName} WHERE {IdColumnName} IN @Ids";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(query, new { Ids = ids });
        }
        // Kiểm tra trùng lặp giá trị của một property (cột) nào đó, có thể loại trừ một Id nhất định (dành cho update)
        public async Task<bool> CheckDupblicate(string propertyName, object value, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("propertyName is required", nameof(propertyName));

            var prop = _validProperties.FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
            if (prop == null)
                throw new ArgumentException($"Property '{propertyName}' is not a valid column for type {typeof(T).Name}", nameof(propertyName));

            var columnName = GetColumnNameStatic(prop);

            using var connection = _connectionFactory.CreateConnection();
            // Nếu value là null thì không check nữa
            if (value == null) return false;
            // Nếu value không null thì dùng = để so sánh
            var query = $"SELECT COUNT(1) FROM {TableName} WHERE {columnName} = @Value";
            if (excludeId.HasValue)
            {
                query += $" AND {IdColumnName} <> @ExcludeId";
                var cnt = await connection.ExecuteScalarAsync<int>(query, new { Value = value, ExcludeId = excludeId.Value });
                return cnt > 0;
            }
            var cntDefault = await connection.ExecuteScalarAsync<int>(query, new { Value = value });
            return cntDefault > 0;
        }
    }
}