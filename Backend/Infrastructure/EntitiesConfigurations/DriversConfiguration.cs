using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class DriversConfiguration : IEntityTypeConfiguration<Drivers>
    {
        public void Configure(EntityTypeBuilder<Drivers> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasOne<Teams>().WithMany().HasForeignKey(d => d.TeamId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(d => d.Country).IsRequired();
            builder.Property(d => d.Name).IsRequired();

        }
    }
}
