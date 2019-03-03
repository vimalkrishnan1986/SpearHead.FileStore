using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Models;
using SpearHead.FileStore.Api.Filters;
using SpearHead.FileStore.Common.Logging;
using SpearHead.FileStore.Api.ResponseResults;

namespace SpearHead.FileStore.Api.Controllers
{
    [RoutePrefix("filestore/api")]
    //[Authorize]
    public sealed class FileStoreController : BaseController
    {
        private readonly IFileBusinessService _fileBusinessService;

        public FileStoreController(IFileBusinessService fileBusinessService, ILoggingService loggingService)
            : base(loggingService)
        {
            _fileBusinessService = fileBusinessService ?? throw new ArgumentNullException(nameof(fileBusinessService));

        }

        [HttpPost]
        [Route("upload")]
        [ValidateModel]
        public async Task<IHttpActionResult> UploadModel([FromBody] FileModel fileModel)
        {
            _LoggingService.Log($"Request has been recieved , correlationId {CorrelationId}");
            int id = await _fileBusinessService.UploadFile(fileModel);
            return new StatusCodeResult<object>(id, HttpStatusCode.Created);
        }

        [HttpGet]
        [Route("download/{id}")]
        public async Task<IHttpActionResult> Get([FromUri] int id)
        {
            _LoggingService.Log($"Request has been recieved , correlationId {CorrelationId}");
            return new StatusCodeResult<FileModel>(await _fileBusinessService.Dowload(id), HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            _LoggingService.Log($"Request has been recieved , correlationId {CorrelationId}");
            await _fileBusinessService.Delete(id);
            return new StatusCodeResult<string>("Deleted", HttpStatusCode.Accepted);
        }
    }
}
