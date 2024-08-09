using BaseInsightDotNet.Commons.Base;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //void BeginTransaction();
        //Task BeginTransactionAsync();
        //int SaveChanges();
        //Task<int> SaveChangesAsync();
        //void Rollback();
        //Task RollbackTransactionAsync();
        //void CommitTransaction();
        //Task CommitTransactionAsync();
        //IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        IRepository<T> Repository<T>() where T : BaseEntity;

        void SaveChanges();

        Task SaveChangesAsync();
        string GetUserIdByUserName(string userName);
        IDbContextTransaction BeginTransaction();
    }
}
