using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class TreeConfiguration : IEntityTypeConfiguration<Tree>
    {
        public void Configure(EntityTypeBuilder<Tree> builder)
        {
            builder.Property(e => e.son)
                .HasColumnName("children");

            builder.Property(e => e.parent)
                .HasColumnName("parent");

            builder.Property(e => e.title)
                .HasColumnName("title");

            builder.HasNoKey().ToView(null);
        }
    }
}
