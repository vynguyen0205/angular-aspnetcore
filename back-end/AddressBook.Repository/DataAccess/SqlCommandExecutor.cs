using System.Data;
using System.Threading.Tasks;
using AddressBook.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AddressBook.DataRepository.DataAccess
{
    public class SqlCommandExecutor : ISqlCommandExecutor
    {
        private readonly ApplicationDbContext _dbContext;

        public SqlCommandExecutor(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            await _dbContext.Database.ExecuteSqlCommandAsync(query, parameters);
        }

        public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            _dbContext.Database.ExecuteSqlCommand(query, parameters);
        }

        public async Task<DataTable> GetDataByCustomCommandAsync(string sql)
        {
            var data = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    var reader = await command.ExecuteReaderAsync();

                    data.Load(reader);

                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return data;
        }

        public DataTable GetDataByCustomCommand(string sql)
        {
            var data = new DataTable();
            var conn = _dbContext.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;

                    var reader = command.ExecuteReader();

                    data.Load(reader);

                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }

            return data;
        }
    }
}
