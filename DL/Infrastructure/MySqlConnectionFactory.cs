using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Data;

namespace DL
{
    public class MySqlConnectionFactory : Interface.IDbConnectionFactory
    {
        private readonly string _connectionString;

        public MySqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Misa2Thang")
                ?? throw new InvalidOperationException("Connection string not found");
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
