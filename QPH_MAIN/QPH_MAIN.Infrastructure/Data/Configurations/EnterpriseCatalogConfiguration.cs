using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;
using System.Security.Cryptography.X509Certificates;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class EnterpriseCatalogConfiguration : IEntityTypeConfiguration<EnterpriseCatalog>
    {
        public void Configure(EntityTypeBuilder<EnterpriseCatalog> builder)
        {
            builder.ToTable("EnterpriseCatalog");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_enterprise)
                .HasColumnName("id_enterprise");

            builder.Property(e => e.id_catalog)
                .HasColumnName("id_catalog");

            builder.HasOne(d => d.enterprise)
                .WithMany(p => p.enterpriseCatalog)
                .HasForeignKey(d => d.enterprise)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_EnterpriseCatalog_Enterprise");

            builder.HasOne(d => d.catalog)
                .WithMany(p => p.enterpriseCatalog)
                .HasForeignKey(d => d.catalog)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_EnterpriseCatalog_Catalog");
        }
    }
}
