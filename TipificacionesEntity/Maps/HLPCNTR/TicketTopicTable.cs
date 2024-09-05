using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipificacionesEntity.Maps.HLPCNTR
{
    [Table("TicketTopic", Schema = "HLPCNTR")]
    public class TicketTopicTable
    {
        [Key, Column(Order = 1)]
        public int TicketTopicID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string CodigoTemaCRM { get; set; }
    }
}

