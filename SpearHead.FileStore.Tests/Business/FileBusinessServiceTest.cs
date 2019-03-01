using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpearHead.FileStore.Data.Contracts;
using SpearHead.FileStore.Data.Repositories;
using SpearHead.FileStore.Data;
using SpearHead.FileStore.Data.Entities;
using System.Configuration;
using SpearHead.FileStore.DataServices.Contracts;
using SpearHead.FileStore.DataServices;
using SpearHead.FileStore.Common.Logging;
using SpearHead.FileStore.Common.Helpers;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.BusinessServices;
using SpearHead.FileStore.Models;
using SpearHead.FileStore.Common.Exceptions;
using FluentAssertions;

namespace SpearHead.FileStore.Tests.Business
{
    [TestClass]
    public class FileBusinessServiceTest
    {
        IFileBusinessService _fileBusinessService;

        [TestInitialize]
        public void Test_Initilize()
        {
            ILoggingService _loggingService;
            IRepository<FileMetaData> _repository;
            IFileDataService _fileDataService;
            _loggingService = new LoggingService();
            var context = new Context(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString);
            _repository = new FileMetaDataRepository(context);
            _fileDataService = new FileDataService(_repository);
            _fileBusinessService = new FileBusinessService(_loggingService, _fileDataService);
        }

        [TestMethod]
        public async Task FileBusinessService_UploadTest()
        {
            var model = new FileModel()
            {
                Name = "test.xlsx",
                FileBytes = FileHelper.ReadBytes($"TestData/Sample.xlsx")
            };

            var id = await _fileBusinessService.UploadFile(model);
            id.Should().BeGreaterThan(0);
            var model1 = await _fileBusinessService.Dowload(id);
            model.Name.Should().Be(model.Name);
            model.FileBytes.Should().NotBeNull();
            model.FileBytes.Length.Should().Be(model.FileBytes.Length);
            await _fileBusinessService.Delete(id);
        }
        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task FileBusinessService_Delete_Test()
        {
            var model = new FileModel()
            {
                Name = "test.xlsx",
                FileBytes = FileHelper.ReadBytes($"TestData/Sample.xlsx")
            };

            var id = await _fileBusinessService.UploadFile(model);
            id.Should().BeGreaterThan(0);
            await _fileBusinessService.Delete(id);
            await _fileBusinessService.Dowload(id);
        }


    }
}
