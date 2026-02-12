using Core.Interfaces;
using Dapper;
using Infrastructure.Data;
using System.Data;


namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public IGenericRepository Repository { get; }

        public UnitOfWork(IDbConnectionFactory factory)
        {
            _connection = factory.CreateConnection();
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            Repository = new GenericRepository(_connection, _transaction);
        }

        public void Commit() => _transaction.Commit();
        public void Rollback() => _transaction.Rollback();

        public void Dispose()
        {
            _transaction.Dispose();
            _connection.Dispose();
        }
    }
}
