using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_role)
                .HasColumnName("id_role");

            builder.Property(e => e.id_enterprise)
                .HasColumnName("id_enterprise");

            builder.Property(e => e.id_country)
                .HasColumnName("id_country");

            builder.Property(e => e.nickname)
                .IsRequired()
                .HasColumnName("nickname");

            builder.Property(e => e.firstName)
                .HasColumnName("firstName");

            builder.Property(e => e.lastName)
                .HasColumnName("lastName");

            builder.Property(e => e.activation_code)
                .HasColumnName("activation_code");

            builder.Property(e => e.email)
                .IsRequired()
                .HasColumnName("email");

            builder.Property(e => e.phone_number)
                .HasColumnName("phone_number");

            builder.Property(e => e.hashPassword)
                .IsRequired()
                .HasColumnName("hashPassword");

            builder.Property(e => e.google_access_token)
                .HasColumnName("google_access_token");

            builder.Property(e => e.facebook_access_token)
                .HasColumnName("facebook_access_token");

            builder.Property(e => e.firebase_token)
                .HasColumnName("firebase_token");

            builder.Property(e => e.is_account_activated)
                .HasColumnName("is_account_activated");

            builder.Property(e => e.profile_picture)
                .HasColumnName("profile_picture");

            builder.Property(e => e.status)
                .HasColumnName("status");

            builder.HasOne(u => u.country)
                .WithMany(p => p.users)
                .HasForeignKey(d => d.id_country)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_Country");

            builder.HasOne(u => u.roles)
                .WithMany(p => p.users)
                .HasForeignKey(d => d.id_role)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_Roles");

            builder.HasOne(d => d.enterprise)
                .WithMany(p => p.users)
                .HasForeignKey(d => d.id_enterprise)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_User_Enterprise");
        }
    }
}