using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class UserViewConfiguration : IEntityTypeConfiguration<UserView>
    {
        public void Configure(EntityTypeBuilder<UserView> builder)
        {
            builder.ToTable("UserView");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.userId)
                .IsRequired()
                .HasColumnName("id_user");

            builder.Property(e => e.parent)
                .IsRequired()
                .HasColumnName("parent");

            builder.Property(e => e.children)
                .IsRequired()
                .HasColumnName("children");

            builder.HasOne(d => d.user)
                .WithMany(p => p.userViews)
                .HasForeignKey(d => d.userId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_View_User");

            builder.HasOne(d => d.views)
                .WithMany(p => p.userView)
                .HasForeignKey(d => d.children)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Children_View");

            builder.HasOne(d => d.views)
                .WithMany(p => p.userView)
                .HasForeignKey(d => d.parent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Parent_View");
        }
    }
}