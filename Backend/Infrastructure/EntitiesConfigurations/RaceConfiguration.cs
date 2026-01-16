using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.RaceCircuit).WithOne(c => c.Race).HasForeignKey<Race>(r => r.CircuitId);
        }
    }
}
