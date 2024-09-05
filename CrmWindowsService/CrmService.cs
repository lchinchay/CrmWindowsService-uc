using CrmWindowsService.Schedulers;
using FluentScheduler;
using Serilog;
using System.ServiceProcess;

namespace CrmWindowsService
{
    public partial class CrmService : ServiceBase
    {
        private readonly ILogger _logger;
        public CrmService()
        {
            // InitializeComponent();
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Message}{NewLine}{Exception}")
           .CreateLogger();
        }

        protected override void OnStart(string[] args)
        {
            var _logger = Log.Logger;
            _logger.Information("Servicio iniciado");

            JobManager.Initialize(new TipificacionesRegistry());
        }

        protected override void OnStop()
        {
            _logger.Information("Servicio detenido");
            JobManager.StopAndBlock();
        }
    }
}
