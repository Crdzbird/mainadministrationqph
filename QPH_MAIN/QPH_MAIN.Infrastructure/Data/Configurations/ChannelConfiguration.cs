using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QPH_MAIN.Core.Entities;

namespace QPH_MAIN.Infrastructure.Data.Configurations
{
    public class ChannelConfiguration : IEntityTypeConfiguration<Channel>
    {
        public void Configure(EntityTypeBuilder<Channel> builder)
        {
            builder.ToTable("Channel");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.Fecha)
                .IsRequired()
                .HasColumnName("fecha");

            builder.Property(e => e.Segmento)
                .IsRequired()
                .HasColumnName("segmento");

            builder.Property(e => e.PuntoEmision)
                .IsRequired()
                .HasColumnName("puntoEmision");

            builder.Property(e => e.Ambiente)
                .IsRequired()
                .HasColumnName("ambiente");

            builder.Property(e => e.Iva)
                .IsRequired()
                .HasColumnName("iva");

            builder.Property(e => e.CodigoProducto)
                .IsRequired()
                .HasColumnName("codigoProducto");

            builder.Property(e => e.NombreProducto)
                .IsRequired()
                .HasColumnName("nombreProducto");

            builder.Property(e => e.CategoriaCliente)
                .IsRequired()
                .HasColumnName("categoriaCliente");

            builder.Property(e => e.CuentaContable)
                .IsRequired()
                .HasColumnName("cuentaContable");

            builder.Property(e => e.GrupoCredito)
                .IsRequired()
                .HasColumnName("grupoCredito");

            builder.Property(e => e.DocumentoElectronico)
                .IsRequired()
                .HasColumnName("documentoElectronico");

            builder.Property(e => e.Relacionado)
                .IsRequired()
                .HasColumnName("relacionado");

            builder.Property(e => e.VendorSeccion)
                .IsRequired()
                .HasColumnName("vendorSeccion");

            builder.Property(e => e.ListaPrecioContado)
                .IsRequired()
                .HasColumnName("listaPrecioContado");

            builder.Property(e => e.ListaPrecioCredito)
                .IsRequired()
                .HasColumnName("listaPrecioCredito");

            builder.Property(e => e.LimiteCredito)
                .IsRequired()
                .HasColumnName("limiteCredito");

            builder.Property(e => e.Uge)
                .IsRequired()
                .HasColumnName("uge");

            builder.Property(e => e.Bodega)
                .IsRequired()
                .HasColumnName("bodega");

            builder.Property(e => e.FormaPago)
                .IsRequired()
                .HasColumnName("formaPago");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status");
        }
    }
}