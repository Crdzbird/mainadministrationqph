using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class ViewsConfiguration : IEntityTypeConfiguration<Views>
    {
        public void Configure(EntityTypeBuilder<Views> builder)
        {
            builder.ToTable("Views");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.code)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("code");

            builder.Property(e => e.name)
                .IsRequired()
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.route)
                .IsRequired()
                .HasColumnName("route")
                .HasMaxLength(300);
        }
    }
}