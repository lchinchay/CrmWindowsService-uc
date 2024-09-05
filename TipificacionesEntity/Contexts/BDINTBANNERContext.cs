using Microsoft.EntityFrameworkCore;
using TipificacionesEntity.Maps.HLPCNTR;

namespace TipificacionesEntity.Contexts
{
    public class BDINTBANNERContext : DbContext
    {
        public DbSet<TicketCategoryTable> TicketCategory { get; set; }
        public DbSet<TicketTopicTable> TicketTopic { get; set; }
        public DbSet<TicketCanalTable> TicketCanal { get; set; }
        public DbSet<TicketTypeTable> TicketType { get; set; }

        public BDINTBANNERContext(DbContextOptions<BDINTBANNERContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.EnableThreadSafetyChecks(false);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

