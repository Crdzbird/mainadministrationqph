using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class SystemParametersConfiguration : IEntityTypeConfiguration<SystemParameters>
    {
        public void Configure(EntityTypeBuilder<SystemParameters> builder)
        {
            builder.ToTable("SystemParameters");

            builder.HasKey(e => e.Code);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasColumnName("code");

            builder.Property(e => e.description)
                .IsRequired()
                .HasColumnName("description");

            builder.Property(e => e.value)
                .IsRequired()
                .HasColumnName("value");

            builder.Property(e => e.dataType)
                .IsRequired()
                .HasColumnName("dataType");

            builder.Property(e => e.status)
                .IsRequired()
                .HasColumnName("status");
        }
    }
}
