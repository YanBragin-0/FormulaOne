using FormulaOne.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormulaOne.Infrastructure.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");
            builder.Property(b => b.UserName).HasMaxLength(50);
            builder.Property(b => b.PhoneNumber).HasMaxLength(20);
        }
    }
}
