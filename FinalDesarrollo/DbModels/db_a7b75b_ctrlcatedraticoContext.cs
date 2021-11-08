using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace FinalDesarrollo.DbModels
{
    public partial class ctrlCatedraticosContext : DbContext
    {
        public ctrlCatedraticosContext()
        {
        }

        public ctrlCatedraticosContext(DbContextOptions<ctrlCatedraticosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catedraticos> Catedratico { get; set; }
        public virtual DbSet<Asignacion> Asignacioncurso { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Catedraticos>(entity =>
            {
                entity.ToTable("Catedratico");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Asignacion>(entity =>
            {
                entity.ToTable("Asignacion");

                entity.Property(e => e.ExFinal).HasColumnType("decimal(38, 0)");

                entity.Property(e => e.Notaalumnos).HasColumnType("decimal(38, 0)");

                entity.Property(e => e.Zonaalumnos).HasColumnType("decimal(38, 0)");

                entity.HasOne(d => d.Catedratico)
                    .WithMany(p => p.Asignacions)
                    .HasForeignKey(d => d.CatedraticoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Asignacion_FK");

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Asignacions)
                    .HasForeignKey(d => d.CursoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Asignacion_FK_1");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.ToTable("Curso");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
