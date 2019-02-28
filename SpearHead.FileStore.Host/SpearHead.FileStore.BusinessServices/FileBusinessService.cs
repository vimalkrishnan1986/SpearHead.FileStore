using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Models;
using System.Threading.Tasks;

namespace SpearHead.FileStore.BusinessServices
{
    public sealed class FileBusinessService : IFileBusinessService
    {
        public Task Delete(object Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<FileModel> Dowload(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task UploadFile(FileModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
