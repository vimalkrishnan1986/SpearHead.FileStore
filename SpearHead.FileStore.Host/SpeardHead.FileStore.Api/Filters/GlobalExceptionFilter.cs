using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using SpearHead.FileStore.Common.Exceptions;

namespace SpearHead.FileStore.Api.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotFoundException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(context.Exception?.Message)
                };
                return;
            }
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception?.Message)
            };
        }

    }
}
