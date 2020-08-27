using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class PermissionStatusConfiguration : IEntityTypeConfiguration<PermissionStatus>
    {
        public void Configure(EntityTypeBuilder<PermissionStatus> builder)
        {
            builder.Property(e => e.id)
                .HasColumnName("id");

            builder.Property(e => e.statuses)
                .HasColumnName("status")
                .HasColumnType("int");

            builder.Property(e => e.permission)
                .HasColumnName("permission");

            builder.HasNoKey().ToView(null);
        }
    }
}
