using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class DriverCShConfiguration : IEntityTypeConfiguration<DriverChampionship>
    {
        public void Configure(EntityTypeBuilder<DriverChampionship> builder)
        {
            builder.HasKey(dc => dc.Id);
            builder.HasOne<Drivers>().WithMany().HasForeignKey(dc => dc.DriverId);
            builder.HasOne(dc => dc.Season).WithOne(s => s.DriverChampionship).HasForeignKey<DriverChampionship>(dc => dc.SeasonId);
        }
    }
}
