using System;
using System.Linq;
using System.Web.Http;
using SpearHead.FileStore.Common.Logging;
using SpearHead.FileStore.Api.Filters;

namespace SpearHead.FileStore.Api.Controllers
{
    [RoutePrefix("fileManager/api")]
    [GlobalExceptionFilter]
    public abstract class BaseController : ApiController
    {
        protected readonly ILoggingService _LoggingService;

        protected Guid CorrelationId
        {
            get
            {
                const string key = "CorrelationId";
                var _correlationId = GetHeader(key);
                return string.IsNullOrEmpty(_correlationId) ? Guid.NewGuid() : Guid.Parse(_correlationId);
            }
        }
        protected BaseController(ILoggingService loggingService)
        {
            _LoggingService = loggingService ?? throw new ArgumentNullException(nameof(loggingService));
        }


        protected string GetHeader(string Key)
        {
            var headers = Request.Headers;
            if (headers == null)
            {
                return string.Empty;
            }
            if (!headers.Contains(Key))
            {
                return string.Empty;
            }
            return headers.GetValues(Key).FirstOrDefault();
        }

    }
}
