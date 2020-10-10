using ATM_Origin.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATM_Origin_Infrastucture.Configurations
{
    public class OperacionesConfiguration : IEntityTypeConfiguration<Operaciones>
    {
        public void Configure(EntityTypeBuilder<Operaciones> builder)
        {
            builder.HasKey(e => e.Id)
                    .IsClustered(false);

            builder.ToTable("OPERACIONES");

            builder.Property(e => e.Balance).HasColumnType("numeric(32, 2)");

            builder.Property(e => e.CodigoOperacion)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

            builder.Property(e => e.Fecha).HasColumnType("datetime");

            builder.HasOne(d => d.Tarjeta)
                    .WithMany(p => p.Operaciones)
                    .HasForeignKey(d => d.TarjetaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TARJETA_OPERACIONES");
        }
    }
}
