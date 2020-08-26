using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class ViewCardConfiguration : IEntityTypeConfiguration<ViewCard>
    {
        public void Configure(EntityTypeBuilder<ViewCard> builder)
        {
            builder.ToTable("ViewCard");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.id_view)
                .HasColumnName("id_view");

            builder.Property(e => e.id_card)
                .HasColumnName("id_card");

            builder.HasOne(d => d.card)
                .WithMany(p => p.viewCard)
                .HasForeignKey(d => d.id_card)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ViewCard_Card");

            builder.HasOne(d => d.view)
                .WithMany(p => p.viewCard)
                .HasForeignKey(d => d.id_view)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ViewCard_View");
        }
    }
}
