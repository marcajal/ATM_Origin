using ATM_Origin.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin_Infrastucture.Configurations
{
    public class TarjetaConfiguration : IEntityTypeConfiguration<Tarjeta>
    {
        public void Configure(EntityTypeBuilder<Tarjeta> builder)
        {
            builder.ToTable("TARJETA");

            builder.HasIndex(e => e.Numero)
                .HasName("UQ__TARJETA__7E532BC699C6476B")
                .IsUnique();

            builder.Property(e => e.Balance).HasColumnType("numeric(32, 2)");

            builder.Property(e => e.FechaVto).HasColumnType("datetime");

            builder.Property(e => e.Numero)
                .IsRequired()
                .HasMaxLength(16)
                .IsUnicode(false);

            builder.Property(e => e.Pin)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.Habilitada).HasColumnType("bit");
        }
    }
}
