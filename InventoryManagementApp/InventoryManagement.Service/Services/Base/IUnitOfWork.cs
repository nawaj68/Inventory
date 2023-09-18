using InventoryManagement.Core.Sqls;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Service.Services.Base
{
    public interface IUnitOfWork
    {
        // Repositories
        SqlRepository<T> Repository<T>() where T : MasterEntity, new();

        //IAuditTrailRepository AuditTrailRepository { get; }

        int Complete();
        Task<int> CompleteAsync();

        void Dispose();
        Task DisposeAsync();

        Task<IDbContextTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        IDbContextTransaction CreateTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
