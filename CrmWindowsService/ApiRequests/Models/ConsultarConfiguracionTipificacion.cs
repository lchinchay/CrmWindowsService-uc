namespace CrmWindowsService.ApiRequests.Models
{
    public class ConsultarConfiguracionTipificacion
    {
        public string CodigoCanal { get; set; }
        public string NombreCanal { get; set; }
        public string CodigoTipoAtencion { get; set; }
        public string NombreTipoAtencion { get; set; }
        public string CodigoCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public string CodigoSubCategoria { get; set; }
        public string NombreSubCategoria { get; set; }
        public string CodigoTema { get; set; }
        public string NombreTema { get; set; }
        public string CodigoTramite { get; set; }
        public string NombreTramite { get; set; }
        public bool? Estado { get; set; }
    }
}

