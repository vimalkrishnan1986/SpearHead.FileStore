using System;
using Microsoft.Owin.Hosting;
using SpearHead.FileStore.Api.App_Start;
using SpearHead.FileStore.Common.Helpers;

namespace SpearHead.FileStore.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string port = ConfigHelper.GetConfigValue<string>("UploadServicePort");

            StartOptions options = new StartOptions();
            options.Urls.Add(string.Format("http://localhost:{0}", port));
            options.Urls.Add(string.Format("http://127.0.0.1:{0}", port));
            string machineName = Environment.MachineName;
            try
            {
                options.Urls.Add(string.Format("http://{0}:{1}", "*", port));
            }
            catch (Exception ex)
            {
                options.Urls.Add(string.Format("http://{0}:{1}", machineName, port));
                Console.Write(ex.Message);
            }
            WebApp.Start<Startup>(options);
            Console.WriteLine("== Server Endpoints == ");
            Console.WriteLine("Server endpoint at {0}", options.Urls[0]);
            Console.WriteLine("Server endpoint at {0}", options.Urls[1]);
            Console.WriteLine("Server endpoint at {0}", options.Urls[2]);
            Console.WriteLine(string.Empty);
            Console.WriteLine("Web Server is now running, press Ctrl+C to quit.");

            Console.ReadLine();
        }
    }
}
