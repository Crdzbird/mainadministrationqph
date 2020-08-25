using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class UserCardGrantedConfiguration : IEntityTypeConfiguration<UserCardGranted>
    {
        public void Configure(EntityTypeBuilder<UserCardGranted> builder)
        {
            builder.ToTable("UserCardGranted");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_user)
                .HasColumnName("id_user");

            builder.Property(e => e.id_card)
                .HasColumnName("id_card");

            builder.HasOne(d => d.cards)
                .WithMany(p => p.userCardGranted)
                .HasForeignKey(d => d.id_card)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserCardGranted_Card");

            builder.HasOne(d => d.user)
                .WithMany(p => p.userCardGranted)
                .HasForeignKey(d => d.id_user)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_UserCardGranted_User");
        }
    }
}
