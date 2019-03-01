using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearHead.FileStore.Data.Contracts
{
    public interface IRepository<T>
    {
        Task AddAsync(T obj);
        Task DeleteAsync(object id);
        Task<T> GetByIdAsync(object id);
    }
}
