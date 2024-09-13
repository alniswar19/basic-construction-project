using BCI.Domain.Config;
using BCI.Domain.Repositories;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace BCI.Infrastructure.Persistances
{
    public class DbFactory : IDbConnectionFactory
    {
        private readonly AppConfig _configuration;

        public DbFactory(IOptions<AppConfig> configuration)
        {
            _configuration = configuration.Value;
        }

        /// <summary>
        /// It returns new SQL connection, but under the hood ADO.NET will return the existing open connection
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return new SqlConnection(_configuration.ConnectionString);
        }
    }
}
