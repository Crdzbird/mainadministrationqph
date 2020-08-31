using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;
using System.Security.Cryptography.X509Certificates;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class EnterpriseHierarchyCatalogConfiguration : IEntityTypeConfiguration<EnterpriseHierarchyCatalog>
    {
        public void Configure(EntityTypeBuilder<EnterpriseHierarchyCatalog> builder)
        {
            builder.ToTable("EnterpriseHierarchyCatalog");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_enterprise)
                .HasColumnName("id_enterprise");

            builder.Property(e => e.parent)
                .HasColumnName("parent");

            builder.Property(e => e.children)
                .HasColumnName("children");

            builder.HasOne(d => d.enterprise)
                .WithMany(p => p.enterpriseCatalog)
                .HasForeignKey(d => d.id_enterprise)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_EnterpriseCatalog_Enterprise");

            builder.HasOne(d => d.catalog)
                .WithMany(p => p.enterpriseCatalog)
                .HasForeignKey(d => d.children)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Children_Catalog");

            builder.HasOne(d => d.catalog)
                .WithMany(p => p.enterpriseCatalog)
                .HasForeignKey(d => d.parent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Parent_Catalog");
        }
    }
}
