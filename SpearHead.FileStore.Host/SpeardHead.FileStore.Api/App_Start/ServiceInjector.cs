using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Microsoft.Extensions.DependencyInjection;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Common.Logging;
using SpearHead.FileStore.BusinessServices;

namespace SpearHead.FileStore.Api.App_Start
{
    public static class ServiceInjector
    {

        public static IServiceProvider ConfigureServices(ServiceCollection services)
        {
            var controllerTypes = Assembly.GetExecutingAssembly().GetExportedTypes()
             .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
             .Where(t => typeof(ApiController).IsAssignableFrom(t) ||
             t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase));
            foreach (var type in controllerTypes)
            {
                services.AddTransient(type);
            }
            AddDependencies(services);
            return services.BuildServiceProvider();
        }

        private static void AddDependencies(ServiceCollection services)
        {
            services.AddTransient<IFileBusinessService, FileBusinessService>();
            services.AddTransient<ILoggingService, LoggingService>();
        }
    }
}
