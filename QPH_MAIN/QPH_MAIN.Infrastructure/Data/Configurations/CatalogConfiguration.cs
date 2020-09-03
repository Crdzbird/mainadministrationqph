using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.ToTable("Catalog");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.code)
                .IsRequired()
                .HasColumnName("code");

            builder.Property(e => e.name)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(e => e.description)
                .IsRequired()
                .HasColumnName("description");

            builder.Property(e => e.status)
                .IsRequired()
                .HasColumnName("status");
        }
    }
}
