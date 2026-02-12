using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sp, object param = null, CommandType commandType = CommandType.StoredProcedure);
        Task<T> QuerySingleAsync<T>(string sp, object param = null, CommandType commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync(string sp, object param = null, CommandType commandType = CommandType.StoredProcedure);
    }
}
