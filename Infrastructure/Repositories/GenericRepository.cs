using Core.Interfaces;
using Dapper;
using System.Data;


namespace Infrastructure.Repositories
{
   
    public class GenericRepository : IGenericRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public GenericRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sp, object param = null, CommandType commandType=CommandType.StoredProcedure)
            => await _connection.QueryAsync<T>(sp, param, _transaction, commandType: commandType);

        public async Task<T> QuerySingleAsync<T>(string sp, object param = null, CommandType commandType = CommandType.StoredProcedure)
            => await _connection.QueryFirstOrDefaultAsync<T>(sp, param, _transaction, commandType: commandType);

        public async Task<int> ExecuteAsync(string sp, object param = null, CommandType commandType = CommandType.StoredProcedure)
            => await _connection.ExecuteAsync(sp, param, _transaction, commandType: commandType);
    }

}
