using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_region)
                .HasColumnName("id_region");

            builder.Property(e => e.name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.region)
                .WithMany(p => p.city)
                .HasForeignKey(d => d.id_region)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_City_Region");
        }
    }
}
