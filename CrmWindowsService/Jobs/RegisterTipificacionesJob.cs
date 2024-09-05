using CrmWindowsService.ApiRequests;
using CrmWindowsService.ApiRequests.Models;
using CrmWindowsService.Jobs.Workers;
using FluentScheduler;
using Serilog;
using System.Collections.Generic;

namespace CrmWindowsService.Jobs
{
    public class RegisterTipificacionesJob : IJob
    {
        public void Execute()
        {
            Log.Information("======================= RegisterTipificacionesJob se ejecuta =======================");
            TipificacionesAPIs _tipificacionesAPIs = new TipificacionesAPIs();

            List<ConsultarConfiguracionTipificacion> configuracionTipificaciones =
                _tipificacionesAPIs.getDataTipificaciones();

            RegisterTipificaciones _registerTipificaciones = new RegisterTipificaciones();
            Log.Information("Procesando data ...");
            foreach (ConsultarConfiguracionTipificacion tip in configuracionTipificaciones)
            {
                _registerTipificaciones.ProcessTipificaciones(tip);
            }


            Log.Information("======================= RegisterTipificacionesJob terminó de ejecutar =======================");
        }
    }
}

