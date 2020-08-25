using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class CardsConfiguration : IEntityTypeConfiguration<Cards>
    {
        public void Configure(EntityTypeBuilder<Cards> builder)
        {
            builder.ToTable("Cards");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.card)
                .IsRequired()
                .HasColumnName("card")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
