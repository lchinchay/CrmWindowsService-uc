using Serilog;
using System;
using System.ServiceProcess;

namespace CrmWindowsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        static void Main()
        {
            Console.WriteLine("Iniciando el servicio...");

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new CrmService()
            };
            ServiceBase.Run(ServicesToRun);
        }

        private static void ConfigureLogging()
        {            
            Log.Logger = new LoggerConfiguration()
                //.ReadFrom.AppSettings()
                .CreateLogger();

            Log.Information("Logger configurado correctamente.");
        }
    }
}
