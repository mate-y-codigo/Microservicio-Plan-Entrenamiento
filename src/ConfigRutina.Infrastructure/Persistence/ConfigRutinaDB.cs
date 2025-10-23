using Microsoft.EntityFrameworkCore;
using ConfigRutina.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigRutina.Infrastructure.Data
{
    public class ConfigRutinaDB : DbContext
    {
        public DbSet<CategoriaEjercicio> CategoriaEjercicios { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<EjercicioSesion> EjercicioSesiones { get; set; }
        public DbSet<SesionEntrenamiento> SesionEntrenamientos { get; set; }
        public DbSet<PlanEntrenamiento> PlanEntrenamientos { get; set; }
        public ConfigRutinaDB(DbContextOptions<ConfigRutinaDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // relaciones
            modelBuilder.Entity<Ejercicio>()
                .HasOne<CategoriaEjercicio>(e => e.CategoriaEjercicioEn)
                .WithMany(c => c.EjercicioLista)
                .HasForeignKey(e => e.IdCategoriaEjercicio)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EjercicioSesion>(entity =>
            {
                entity.HasOne<SesionEntrenamiento>(e => e.SesionEntrenamientoEn)
                .WithMany(s => s.EjercicioSesionLista)
                .HasForeignKey(e => e.IdSesionEntrenamiento)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Ejercicio>(es => es.EjercicioEn)
                .WithMany(e => e.EjercicioSesionLista)
                .HasForeignKey(es => es.IdEjercicio)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SesionEntrenamiento>()
                .HasOne<PlanEntrenamiento>(se => se.PlanEntrenamientoEn)
                .WithMany(pe => pe.SesionEntrenamientoLista)
                .HasForeignKey(se => se.IdPlanEntrenamiento)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanEntrenamiento>(entity =>
            {
                entity.ToTable("PlanEntrenamiento");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(100)").IsRequired();
                entity.Property(e => e.Descripcion).HasColumnType("TEXT").IsRequired();
                entity.Property(e => e.EsPlantilla).HasColumnType("BOOL").IsRequired();
                entity.Property(e => e.FechaCreacion).HasColumnType("TIMESTAMPTZ").IsRequired();
                entity.Property(e => e.FechaActualizacion).HasColumnType("TIMESTAMPTZ").IsRequired();
                entity.Property(e => e.Activo).HasColumnType("BOOL").IsRequired();
            });

            modelBuilder.Entity<SesionEntrenamiento>(entity =>
            {
                entity.ToTable("SesionEntrenamiento");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(100)").IsRequired();
                entity.Property(e => e.Orden).HasColumnType("INT").IsRequired();
            });

            modelBuilder.Entity<EjercicioSesion>(entity =>
            {
                entity.ToTable("EjercicioSesion");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.SeriesObjetivo).HasColumnType("INT").IsRequired();
                entity.Property(e => e.RepeticionesObjetivo).HasColumnType("INT").IsRequired();
                entity.Property(e => e.PesoObjetivo).HasColumnType("FLOAT").IsRequired();
                entity.Property(e => e.Descanso).HasColumnType("INT").IsRequired();
                entity.Property(e => e.Orden).HasColumnType("INT").IsRequired();
            });

            modelBuilder.Entity<Ejercicio>(entity =>
            {
                entity.ToTable("Ejercicio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(100)").IsRequired();
                entity.Property(e => e.MusculoPrincipal).HasColumnType("VARCHAR(50)").IsRequired();
                entity.Property(e => e.GrupoMuscular).HasColumnType("VARCHAR(50)").IsRequired();
                entity.Property(e => e.UrlDemo).HasColumnType("TEXT").IsRequired();
                entity.Property(e => e.Activo).HasColumnType("BOOL").IsRequired();
            });

            modelBuilder.Entity<CategoriaEjercicio>(entity =>
            {
                entity.ToTable("CategoriaEjercicio");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(25)").IsRequired();
            });

            modelBuilder.Entity<CategoriaEjercicio>().HasData(
                new CategoriaEjercicio { Id = 1, Nombre = "Movilidad" },
                new CategoriaEjercicio { Id = 2, Nombre = "Calentamiento" },
                new CategoriaEjercicio { Id = 3, Nombre = "Fuerza" },
                new CategoriaEjercicio { Id = 4, Nombre = "Hipertrofia" }
                );
        }
    }
}
