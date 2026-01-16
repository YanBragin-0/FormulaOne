using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class CircuitConfiguration : IEntityTypeConfiguration<RaceCircuit>
    {
        public void Configure(EntityTypeBuilder<RaceCircuit> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Title).IsRequired();
            builder.Property(c => c.Length).IsRequired();
            builder.Property(c => c.CountryLocation).IsRequired();
        }
    }
}
