using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace SpearHead.FileStore.Api.App_Start
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider provider;

        public DefaultDependencyResolver(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public object GetService(Type serviceType)
        {
            return provider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return provider.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }
}