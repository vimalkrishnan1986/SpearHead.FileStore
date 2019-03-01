using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpearHead.FileStore.Data.Entities;

namespace SpearHead.FileStore.DataServices.Contracts
{
    public interface IFileDataService
    {
        Task AddAsync(FileMetaData fileMetaData);
        Task<FileMetaData> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
