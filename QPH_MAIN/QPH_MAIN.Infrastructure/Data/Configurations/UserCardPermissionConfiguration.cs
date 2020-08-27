using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class UserCardPermissionConfiguration : IEntityTypeConfiguration<UserCardPermission>
    {
        public void Configure(EntityTypeBuilder<UserCardPermission> builder)
        {
            builder.ToTable("UserCardPermissions");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_permission)
                .HasColumnName("id_permission");

            builder.Property(e => e.id_card_granted)
                .HasColumnName("id_card_granted");

            builder.HasOne(d => d.userCardGranted)
                .WithMany(p => p.userCardPermission)
                .HasForeignKey(d => d.id_card_granted)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserCardPermission_UserCardGranted");

            builder.HasOne(d => d.permissions)
                .WithMany(p => p.userCardPermissions)
                .HasForeignKey(d => d.id_permission)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserCardPermission_Permission");
        }
    }
}
