using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(p => p.id);

            builder.Property(p => p.Nombre)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.Property(p => p.Apellido)
                   .IsRequired()
                   .HasMaxLength(80);

            builder.Property(p => p.FechaNacimiento)
                   .IsRequired();

            builder.Property(p => p.Telefono)
                   .IsRequired()
                   .HasMaxLength(9);

            builder.Property(p => p.Email)
                   .HasMaxLength(100);

            builder.Property(p => p.Direccion)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.Property(p => p.Edad);

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(30);

            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(30);
        }
    }
}
