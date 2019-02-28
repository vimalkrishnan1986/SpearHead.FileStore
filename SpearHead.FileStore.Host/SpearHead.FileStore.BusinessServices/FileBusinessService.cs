using System.Threading.Tasks;
using System;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Models;
using SpearHead.FileStore.DataServices.Contracts;
using SpearHead.FileStore.Common.Logging;
using SpearHead.FileStore.Common.Helpers;
using SpearHead.FileStore.Data.Entities;
using SpearHead.FileStore.Common.Exceptions;

namespace SpearHead.FileStore.BusinessServices
{
    public sealed class FileBusinessService : IFileBusinessService
    {
        private readonly IFileDataService _fileDataService;
        private readonly ILoggingService _loggingService;
        private const string basePathKey = "StorageLocation";

        private string BasePath
        {
            get
            {
                return ConfigHelper.GetConfigValue<string>(basePathKey);
            }
        }



        public FileBusinessService(ILoggingService loggingService, IFileDataService fileDataService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
            _fileDataService = fileDataService ?? throw new ArgumentNullException(nameof(fileDataService));
        }

        public async Task Delete(object id)
        {
            var metaData = await _fileDataService.GetAsync(int.Parse(id.ToString()));
            if (metaData == null)
            {
                throw new NotFoundException($"cCould not find the details for id:{id}");
            }

            string filePath = GetFullFileName(GetDirectoryName(metaData.UploadTime), metaData.SavedFileName);
            try
            {
                await _fileDataService.DeleteAsync(int.Parse(id.ToString()));
                FileHelper.Delete(filePath);
            }
            catch (Exception ex)
            {
                _loggingService.Log(ex);
                throw;
            }
        }

        public async Task<FileModel> Dowload(object id)
        {
            var metaData = await _fileDataService.GetAsync(int.Parse(id.ToString()));
            if (metaData == null)
            {
                throw new NotFoundException($"cCould not find the details for id:{id}");
            }

            string filePath = GetFullFileName(GetDirectoryName(metaData.UploadTime), metaData.SavedFileName);
            try
            {
                return new FileModel
                {
                    FileBytes = FileHelper.ReadBytes(filePath),
                    Name = metaData.ActualFileName
                };
            }
            catch (Exception ex)
            {
                _loggingService.Log(ex);
                throw;
            }
        }

        public async Task UploadFile(FileModel model)
        {
            _loggingService.Log("Request has been recieved for file upload");
            DateTime dateTime = DateTime.UtcNow;
            string directoryName = GetDirectoryName(dateTime);
            _loggingService.Log($"Creating Folder Structure with directory name{directoryName}");
            string path = GetPath(directoryName);

            try
            {
                FileHelper.CreateDirectory(path);
                _loggingService.Log($"Directory {path} has been created");
                string fileName = FileHelper.GetRandomFileName();
                _loggingService.Log($"Saving file {fileName}");
                FileHelper.WriteToFile(model.FileBytes, GetFullFileName(directoryName, fileName));
                _loggingService.Log("Successfully saved to repository");
                await _fileDataService.AddAsync(new FileMetaData
                {
                    ActualFileName = model.Name,
                    SavedFileName = fileName,
                    UploadTime = dateTime
                });
                _loggingService.Log("Metadata is saved to data base");
            }
            catch (Exception ex)
            {
                _loggingService.Log(ex);
                throw;
            }
        }

        private string GetDirectoryName(DateTime dateTime)
        {
            return $"{dateTime.Year}-{dateTime.Month}-{dateTime.DayOfWeek.ToString()}";
        }
        private string GetFullFileName(string directoryName, string fileName)
        {
            return $"{BasePath}//{directoryName}//{fileName}";
        }
        private string GetPath(string directoryName)
        {
            return $"{BasePath}//{directoryName}";
        }

    }
}
