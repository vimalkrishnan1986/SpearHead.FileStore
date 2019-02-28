﻿using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Owin;
using SpearHead.FileStore.Api.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Formatting;
namespace SpearHead.FileStore.Api.App_Start
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            var services = new ServiceCollection();
            var serviceProvider = ServiceInjector.ConfigureServices(services);
            config.DependencyResolver = new DefaultDependencyResolver(serviceProvider);
            config.Filters.Add(new GlobalExceptionFilter());
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            appBuilder.UseWebApi(config);
        }
    }
}
