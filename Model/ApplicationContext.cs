using Microsoft.EntityFrameworkCore;
using ToFEA.Model;

namespace TofEA.Model
{
    public class ApplicationContext : DbContext
    {
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<PossibleStat> PossibleStats { get; set; }
        public DbSet<PossibleTitanStat> PossibleTitanStats { get; set; }
        public DbSet<TitanStat> TitanStats { get; set; }
        public DbSet<BaseStat> BaseStats { get; set; }
        public DbSet<Stat> Stats { get; set; }
        public DbSet<AugmentationStat> AugmentationStats { get; set; }
        public DbSet<StatLink> StatLinks { get; set; }

        internal static bool IsFirstConnect = true;
        
        public ApplicationContext()
        {
            if (IsFirstConnect)
            {
                //Database.EnsureDeleted();
                Database.EnsureCreated();
                IsFirstConnect = false;
            }
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=tofeadata.db");
        }
    }
}
