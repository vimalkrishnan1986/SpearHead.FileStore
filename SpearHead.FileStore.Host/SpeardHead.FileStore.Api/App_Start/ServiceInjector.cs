using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpearHead.FileStore.BusinessServices.Contracts;
using SpearHead.FileStore.Common.Logging;
using SpearHead.FileStore.BusinessServices;
using SpearHead.FileStore.DataServices.Contracts;
using SpearHead.FileStore.Data.Contracts;
using SpearHead.FileStore.DataServices;
using SpearHead.FileStore.Data.Repositories;
using SpearHead.FileStore.Data.Entities;
using System.Data.Entity;
using SpearHead.FileStore.Data;
using SpearHead.FileStore.Common.Helpers;

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
            AddDataDependencies(services);
            return services.BuildServiceProvider();
        }

        private static void AddDependencies(ServiceCollection services)
        {
            services.AddTransient<IFileBusinessService, FileBusinessService>();
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient<IFileDataService, FileDataService>();
        }

        private static void AddDataDependencies(ServiceCollection services)
        {
            services.AddTransient<IRepository<FileMetaData>, FileMetaDataRepository>();
            services.AddSingleton<DbContext>(new Context(ConfigurationManager.ConnectionStrings["ConString"].ConnectionString));
            //dbcontext to be added here
        }
    }
}
