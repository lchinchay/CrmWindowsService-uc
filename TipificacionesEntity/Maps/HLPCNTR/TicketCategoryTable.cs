using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TipificacionesEntity.Maps.HLPCNTR
{
    [Table("TicketCategory", Schema = "HLPCNTR")]
    public class TicketCategoryTable
    {
        [Key, Column(Order = 1)]
        public int TicketCategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int? ParentCategoryID { get; set; }
        public string CodigoCategoriaCRM { get; set; }
        public int? CanalId { get; set; }
    }
}

