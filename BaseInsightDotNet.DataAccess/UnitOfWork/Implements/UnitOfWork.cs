using BaseInsightDotNet.Commons.Base;
using BaseInsightDotNet.DataAccess.Data;
using BaseInsightDotNet.DataAccess.Repository.Implements;
using BaseInsightDotNet.DataAccess.Repository.Interfaces;
using BaseInsightDotNet.DataAccess.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.DataAccess.UnitOfWork.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDbContext _context;
        private Dictionary<Type, object> _repositories;
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContext _dbContext;

        public UnitOfWork(IdentityDbContext context,
            IServiceProvider serviceProvider,
            IDbContext dbContext)
        {
            _context = context;
            _serviceProvider = serviceProvider;
            _repositories = new Dictionary<Type, object>();
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            // Nếu đã có repository cho kiểu T, trả về repository đó
            if (_repositories.ContainsKey(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }

            // Nếu chưa có, tạo mới một repository cho kiểu T
            var repository = new Repository<T>(_context, _dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public string GetUserIdByUserName(string userName)
        {
            return _context.Users.Where(x => x.UserName == userName).FirstOrDefault()?.Id;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }
    }
}
