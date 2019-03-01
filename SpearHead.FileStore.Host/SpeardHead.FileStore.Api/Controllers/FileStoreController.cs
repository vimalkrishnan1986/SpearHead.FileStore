using System;
using System.Threading.Tasks;
using System.Web.Http;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Models;
using SpearHead.FileStore.Api.Filters;
using SpearHead.FileStore.Common.Logging;

namespace SpearHead.FileStore.Api.Controllers
{
    [RoutePrefix("filestore/api")]

    public sealed class FileStoreController : BaseController
    {
        private readonly IFileBusinessService _fileBusinessService;

        public FileStoreController(IFileBusinessService fileBusinessService, ILoggingService loggingService)
            : base(loggingService)
        {
            _fileBusinessService = fileBusinessService ?? throw new ArgumentNullException(nameof(fileBusinessService));

        }

        [HttpPut]
        [Route("upload")]
        [ValidateModel]
        public async Task<IHttpActionResult> UploadModel([FromBody] FileModel fileModel)
        {
            _LoggingService.Log($"Request has been recieved , correlationId {CorrelationId}");
            int id = await _fileBusinessService.UploadFile(fileModel);
            return Ok(id);
        }

        [Route("download/{id}")]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            _LoggingService.Log($"Request has been recieved , correlationId {CorrelationId}");
            return Ok(await _fileBusinessService.Dowload(id));
        }

    }
}
