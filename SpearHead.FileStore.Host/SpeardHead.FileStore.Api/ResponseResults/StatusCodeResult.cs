using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SpearHead.FileStore.Api.ResponseResults
{
    public sealed class StatusCodeResult<T> : IHttpActionResult where T : class
    {
        private readonly T _content;
        private readonly HttpStatusCode _httpStatusCode;

        public StatusCodeResult(T content, HttpStatusCode httpStatusCode)
        {
            _content = content ?? throw new ArgumentNullException(nameof(content));
            _httpStatusCode = httpStatusCode;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(_httpStatusCode)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_content))
            });
        }
    }
}
