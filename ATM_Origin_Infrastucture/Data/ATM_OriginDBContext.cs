using System;
using ATM_Origin.Core.Entities;
using ATM_Origin_Infrastucture.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ATM_Origin_Infrastucture.Data
{
    public partial class ATM_OriginDBContext : DbContext
    {
        public ATM_OriginDBContext()
        {
        }

        public ATM_OriginDBContext(DbContextOptions<ATM_OriginDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Operaciones> Operaciones { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseSqlServer("Server=DESKTOP-J9I7PLT\\SQLEXPRESS;Database=ATM_OriginDB;Integrated Security = true");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OperacionesConfiguration());

            modelBuilder.ApplyConfiguration(new TarjetaConfiguration());

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
