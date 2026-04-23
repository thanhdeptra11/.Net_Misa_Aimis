using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Extension
{
    public class ExtensionUtility
    {
        /// <summary>
        /// Lấy tên bảng từ attribute [ConfigTable]
        /// </summary>
        public static string GetTableName<T>()
        {
            string tableName = (ConfigTableAttribute.GetCustomAttribute(typeof(T), typeof(ConfigTableAttribute)) as ConfigTableAttribute)?.tableName;
            return tableName;
        }

        /// <summary>
        /// Lấy ra đối tượng PropertyInfo của Khóa chính (Primary Key)
        /// Ưu tiên tìm [Key], nếu không có sẽ tự tìm thuộc tính tên là "Id"
        /// </summary>
        public static PropertyInfo GetKeyProperty<T>()
        {
            var properties = typeof(T).GetProperties();

            // 1. Tìm property có gắn [Key]
            var keyProperty = properties.FirstOrDefault(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Any());

            // 2. Fallback: Nếu lập trình viên quên gắn [Key], tự động tìm property tên là "Id" (không phân biệt hoa thường)
            if (keyProperty == null)
            {
                keyProperty = properties.FirstOrDefault(p => p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
            }

            return keyProperty;
        }

        /// <summary>
        /// Lấy tên thuộc tính Khóa chính trong code C# (vd: "Id", "CustomerId")
        /// Dùng để map parameters cho Dapper: @Id, @CustomerId
        /// </summary>
        public static string GetKeyPropertyName<T>()
        {
            var keyProperty = GetKeyProperty<T>();
            return keyProperty?.Name;
        }

        /// <summary>
        /// Lấy tên cột Khóa chính thực tế trong Database (vd: "id", "customer_id")
        /// Dùng để nối chuỗi câu lệnh SQL: WHERE customer_id = @CustomerId
        /// </summary>
        public static string GetKeyColumnName<T>()
        {
            var keyProperty = GetKeyProperty<T>();
            if (keyProperty == null) return null;

            // Kiểm tra xem property này có attribute [Column("tên_cột")] không
            var columnAttribute = keyProperty.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;

            // Nếu có [Column] thì lấy tên trong [Column], nếu không có thì lấy luôn tên Property
            return columnAttribute != null ? columnAttribute.Name : keyProperty.Name;
        }
        public static object? ConvertValue(object? value, string dataType)
        {
            if (value is JsonElement json)
            {
                switch (dataType.ToLower())
                {
                    case "string":
                        return json.GetString();

                    case "int":
                        return json.GetInt32();

                    case "long":
                        return json.GetInt64();

                    case "double":
                        return json.GetDouble();

                    case "bool":
                        return json.GetBoolean();

                    case "datetime":
                        return json.GetDateTime();

                    default:
                        return json.ToString();
                }
            }

            return value;
        }
    }
}