namespace MoneyExchange.Infrastructure.Configuration
{
    using System;
    using Context;
    using System.Data;
    using Transversal.Common;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;

        public UnitOfWork(MoneyExchangeContext context)
        {
            _dbConnection = context.Database.GetDbConnection();
        }

        public IDbTransaction BeginTransaction()
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }

            return _dbConnection.BeginTransaction();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
