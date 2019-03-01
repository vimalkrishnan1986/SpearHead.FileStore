﻿using System.Threading.Tasks;
using SpearHead.FileStore.Models;

namespace SpearHead.FileStore.BusinessServices.Contracts
{
    public interface IFileBusinessService
    {
        Task<int> UploadFile(FileModel model);
        Task Delete(object Id);
        Task<FileModel> Dowload(object id);
    }
}
