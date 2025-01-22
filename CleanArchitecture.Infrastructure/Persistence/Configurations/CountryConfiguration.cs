using CleanArchitecture.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.ToTable("Countries");

            // Set Primary Key
            builder.HasKey(e => e.CountryId);

            // Setting Max Length 2 and Required
            builder.Property(e => e.CountryCode).HasMaxLength(2).IsRequired();
        }
    }
}
