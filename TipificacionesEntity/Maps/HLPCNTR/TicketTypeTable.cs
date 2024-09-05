using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipificacionesEntity.Maps.HLPCNTR
{

    [Table("TicketType", Schema = "HLPCNTR")]
    public class TicketTypeTable
    {
        [Key, Column(Order = 1)]
        public int TicketTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int? TicketSourceID { get; set; }
        public string CodigoTipoAtencionCRM { get; set; }
        public int? CanalId { get; set; }
    }
}
