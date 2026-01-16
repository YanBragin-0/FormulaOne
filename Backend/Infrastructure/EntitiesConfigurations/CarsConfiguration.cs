using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class CarsConfiguration : IEntityTypeConfiguration<Cars>
    {
        public void Configure(EntityTypeBuilder<Cars> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Team).WithOne(t => t.Car).HasForeignKey<Cars>(c => c.TeamId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(c => c.Title).IsRequired();
        }
    }
}
