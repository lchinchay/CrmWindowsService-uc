using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipificacionesEntity.Maps.HLPCNTR
{
    [Table("TicketCanal", Schema = "HLPCNTR")]
    public class TicketCanalTable
    {
        [Key, Column(Order = 1)]
        public int CanalId { get; set; }
        public string Codigo { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public bool? FlagPortal { get; set; }
    }
}

