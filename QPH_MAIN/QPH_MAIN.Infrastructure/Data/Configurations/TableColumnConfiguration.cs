using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class TableColumnConfiguration : IEntityTypeConfiguration<TableColumn>
    {
        public void Configure(EntityTypeBuilder<TableColumn> builder)
        {
            builder.Property(e => e.Code)
                .HasColumnName("column_name");

            builder.Property(e => e.Typename)
                .HasColumnName("typename");

            builder.Property(e => e.MaxLength)
                .HasColumnName("MaxLength");

            builder.HasNoKey().ToView(null);
        }
    }
}
