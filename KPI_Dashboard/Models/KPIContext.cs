using Microsoft.EntityFrameworkCore;

namespace KPI_Dashboard.Models
{
    // Diese Klasse ist für das Lesen von Daten aus der Datenbank zuständig. Die ausgelesenen Daten werden zu 
    // Objekten der Klasse Kpi gemappt

    public partial class KPIContext : DbContext
    {
        public KPIContext()
        {
        }

        public KPIContext(DbContextOptions<KPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kpi> Kpi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        // Ordnet der den Attributen der Klasse Kpi die Daten aus der Datenbank zu
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kpi>(entity =>
            {
                entity.ToTable("rpt_runaudit_2");

                entity.Property(e => e.CellDescription)
                    .IsRequired()
                    .HasColumnName("cellDescription")
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ChangeOverDuration).HasColumnName("changeOverDuration");

                entity.Property(e => e.ChangeOverTargetDuration).HasColumnName("changeOverTargetDuration");

                entity.Property(e => e.Oee)
                    .HasColumnName("oee")
                    .HasColumnType("numeric(38, 6)");

                entity.Property(e => e.PlanUnprodTimeTd)
                    .HasColumnName("planUnprodTimeTd")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.ProdTimeTd)
                    .HasColumnName("prodTimeTd")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.RunStatus)
                    .HasColumnName("runStatus")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.UnprodTimeTd)
                    .HasColumnName("unprodTimeTd")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Util)
                    .HasColumnName("util")
                    .HasColumnType("numeric(18, 4)");

                entity.Property(e => e.Yield)
                    .HasColumnName("yield")
                    .HasColumnType("numeric(18, 4)");
            });
        }

        // Ordnet der den Attributen der Klasse Kpi die Daten aus der Datenbank zu
        public DbSet<KPI_Dashboard.Models.Dashboard> Dashboard { get; set; }
    }
}
