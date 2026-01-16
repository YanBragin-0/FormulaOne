using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class ConstructorsCShConfiguration : IEntityTypeConfiguration<ConstructorsChampionship>
    {
        public void Configure(EntityTypeBuilder<ConstructorsChampionship> builder)
        {
            builder.HasKey(cch => cch.Id);
            builder.HasOne<Teams>().WithMany().HasForeignKey(cch => cch.TeamId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(cch => cch.Season).WithOne(s => s.ConstructorsChampionship).HasForeignKey<ConstructorsChampionship>(cch => cch.SeasonId);
        }
    }
}
