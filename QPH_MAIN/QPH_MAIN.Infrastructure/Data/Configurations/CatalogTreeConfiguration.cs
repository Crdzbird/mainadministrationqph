using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class CatalogTreeConfiguration : IEntityTypeConfiguration<CatalogTree>
    {
        public void Configure(EntityTypeBuilder<CatalogTree> builder)
        {
            builder.Property(e => e.son)
                .HasColumnName("children");

            builder.Property(e => e.parent)
                .HasColumnName("parent");

            builder.Property(e => e.title)
                .HasColumnName("title");

            builder.Property(e => e.code)
                .HasColumnName("code");

            builder.HasNoKey().ToView(null);
        }
    }
}
