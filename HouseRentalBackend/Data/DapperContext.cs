using Npgsql;
using System.Data;
using System.Data.Common;

namespace HouseRentalBackend.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
            => new NpgsqlConnection(connectionString);
    }
}
