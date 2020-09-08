using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;
using System.Security.Cryptography.X509Certificates;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.ToTable("Enterprise");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_city)
                .HasColumnName("id_city");

            builder.Property(e => e.commercial_name)
                .IsRequired()
                .HasColumnName("commercial_name")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.name_application)
                .HasColumnName("name_application");

            builder.Property(e => e.telephone)
                .IsRequired()
                .HasColumnName("telephone")
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.enterprise_address)
                .IsRequired()
                .HasColumnName("enterprise_address")
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.identification)
                .IsRequired()
                .HasColumnName("identification")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.has_branches)
                .HasColumnName("has_branches");

            builder.Property(e => e.latitude)
                .IsRequired()
                .HasColumnName("latitude")
                .HasColumnType("float");

            builder.Property(e => e.longitude)
                .IsRequired()
                .HasColumnName("longitude")
                .HasColumnType("float");

            builder.Property(e => e.created_at)
                .HasColumnName("created_at");

            builder.Property(e => e.status)
                .HasColumnName("status");

            builder.HasOne(d => d.city)
                .WithMany(p => p.enterprise)
                .HasForeignKey(d => d.id_city)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Enterprise_City");

        }
    }
}
