using System.Data;
using System.Threading.Tasks;

namespace AddressBook.Data.Interfaces
{
    public interface ISqlCommandExecutor
    {
        Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters);

        void ExecuteWithStoreProcedure(string query, params object[] parameters);

        Task<DataTable> GetDataByCustomCommandAsync(string sql);

        DataTable GetDataByCustomCommand(string sql);
    }
}
