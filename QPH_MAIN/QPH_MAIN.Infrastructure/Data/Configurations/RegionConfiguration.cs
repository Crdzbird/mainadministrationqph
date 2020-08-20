using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Region");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_country)
                .HasColumnName("id_country");

            builder.Property(e => e.name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.country)
                .WithMany(p => p.regions)
                .HasForeignKey(d => d.id_country)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Country_Region");
        }
    }
}
