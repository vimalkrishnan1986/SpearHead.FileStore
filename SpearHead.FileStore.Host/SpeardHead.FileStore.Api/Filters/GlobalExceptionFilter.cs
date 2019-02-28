using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace SpearHead.FileStore.Api.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(context.Exception?.Message)
            };
        }

    }
}
