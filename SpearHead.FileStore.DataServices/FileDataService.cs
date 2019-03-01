using System;
using System.Threading.Tasks;
using SpearHead.FileStore.DataServices.Contracts;
using SpearHead.FileStore.Data.Contracts;
using SpearHead.FileStore.Data.Entities;

namespace SpearHead.FileStore.DataServices
{
    public sealed class FileDataService : IFileDataService
    {
        private readonly IRepository<FileMetaData> _repository;

        public FileDataService(IRepository<FileMetaData> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task AddAsync(FileMetaData fileMetaData)
        {
            await _repository.AddAsync(fileMetaData);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<FileMetaData> GetAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
