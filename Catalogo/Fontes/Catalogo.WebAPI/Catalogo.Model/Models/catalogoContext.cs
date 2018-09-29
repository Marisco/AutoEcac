using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Catalogo.Model.Models
{
    public partial class catalogoContext : DbContext
    {
        public catalogoContext()
        {
        }

        public catalogoContext(DbContextOptions<catalogoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Arquivo> Arquivo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;Database=catalogo;User=root;Password=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivo>(entity =>
            {
                entity.ToTable("arquivo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descricao).HasColumnType("varchar(255)");

                entity.Property(e => e.Imagem)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Tam).HasColumnType("bigint(20)");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnType("varchar(4)");
            });
        }
    }
}
