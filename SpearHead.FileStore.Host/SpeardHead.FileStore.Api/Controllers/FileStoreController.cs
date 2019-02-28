using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Models;
using SpearHead.FileStore.Api.Filters;
using SpearHead.FileStore.Common.Logging;

namespace SpearHead.FileStore.Api.Controllers
{

    public sealed class FileStoreController : BaseController
    {
        private readonly IFileBusinessService _fileBusinessService;

        public FileStoreController(IFileBusinessService fileBusinessService, ILoggingService loggingService)
            : base(loggingService)
        {
            _fileBusinessService = fileBusinessService ?? throw new ArgumentNullException(nameof(fileBusinessService));

        }

        [ValidateModel]
        public async Task<IHttpActionResult> UploadModel([FromBody] FileModel fileModel)
        {
            _LoggingService.Log($"Request has been recieved , correlationId {CorrelationId}");
            await _fileBusinessService.UploadFile(fileModel);
            return Ok();
        }

    }
}
