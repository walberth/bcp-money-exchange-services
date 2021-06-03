namespace MoneyExchange.Transversal.Common
{
    using System;
    using System.Data;

    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction BeginTransaction();
    }
}
