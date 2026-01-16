using FormulaOne.Entities;
using FormulaOne.Infrastructure.EntitiesConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormulaOne.Infrastructure.Storage
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Teams> Teams { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<RaceCircuit> RaceCircuits { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<DriverChampionship> DriverChampionship { get; set; }
        public DbSet<ConstructorsChampionship> ConstructorsChampionship { get; set; }
        public DbSet<Cars> Cars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarsConfiguration());
            modelBuilder.ApplyConfiguration(new CircuitConfiguration());
            modelBuilder.ApplyConfiguration(new ConstructorsCShConfiguration());
            modelBuilder.ApplyConfiguration(new DriverCShConfiguration());
            modelBuilder.ApplyConfiguration(new DriversConfiguration());
            modelBuilder.ApplyConfiguration(new RaceConfiguration());
            modelBuilder.ApplyConfiguration(new SeasonConfiguration());
            modelBuilder.ApplyConfiguration(new TeamConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
