using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpearHead.FileStore.Data.Contracts;

namespace SpearHead.FileStore.Data.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task AddAsync(T obj)
        {
            _dbContext.Set<T>().Add(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var obj = await GetByIdAsync(id);
            _dbContext.Set<T>().Remove(obj);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
