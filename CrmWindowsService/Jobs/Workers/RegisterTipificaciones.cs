using System.Configuration;
using CrmWindowsService.ApiRequests.Models;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
using TipificacionesEntity.Contexts;
using TipificacionesEntity.Queries;
namespace CrmWindowsService.Jobs.Workers
{
    public class RegisterTipificaciones
    {
        private readonly BDINTBANNERQueries _bdintbannerQueries;

        public RegisterTipificaciones()
        {
            //IConfiguration config = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            //string OutPutThreads = config.GetSection("Logging")["OutPutThreads"];

            string connectionString = ConfigurationManager.ConnectionStrings["BDUCCIConnection"].ConnectionString;


            var optionsBuilder = new DbContextOptionsBuilder<BDINTBANNERContext>();
            optionsBuilder.UseSqlServer(connectionString, options => options.CommandTimeout(300));

            _bdintbannerQueries = new BDINTBANNERQueries(optionsBuilder.Options);
        }

        public void ProcessTipificaciones(ConsultarConfiguracionTipificacion ct)
        {
            //Log.Information($"REGISTRANDO --> {JsonConvert.SerializeObject(ct)}");
            int? canalId = _bdintbannerQueries.setDataTicketCanal(ct.CodigoCanal, ct.NombreCanal);

            if (canalId != null)
            {
                if (ct.CodigoTipoAtencion == "TIP1001" || ct.CodigoTipoAtencion == "TIP1002")
                {
                    _bdintbannerQueries.setDataTicketType(canalId, ct.CodigoTipoAtencion, ct.NombreTipoAtencion, ct.NombreTipoAtencion, ct.Estado ?? false);
                    _bdintbannerQueries.setDataTicketCategory(canalId, ct.CodigoCategoria, ct.NombreCategoria, ct.NombreCategoria, ct.Estado ?? false, ct.CodigoSubCategoria, ct.NombreSubCategoria, ct.NombreSubCategoria, ct.Estado ?? false);
                    _bdintbannerQueries.setDataTicketTopic(ct.CodigoTema, ct.NombreTema, ct.NombreTema, ct.Estado ?? false);
                }
            }
        }
    }
}

