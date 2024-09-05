using CrmWindowsService.Jobs;
using FluentScheduler;
using System.Configuration;
using Serilog;
using System;

namespace CrmWindowsService.Schedulers
{
    public class TipificacionesRegistry : Registry
    {
        public TipificacionesRegistry()
        {
            // Leer configuración desde app.config
            int intervalSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalSeconds"]);

            Log.Warning($"CRON: Intervalo Configurado a {intervalSeconds} segundos.");

            // Configuraciones
            Schedule<RegisterTipificacionesJob>().ToRunNow().AndEvery(intervalSeconds).Seconds();
        }
    }
}
