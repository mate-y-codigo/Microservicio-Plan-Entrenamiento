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
        public DbSet<GrupoMuscular> GruposMusculares { get; set; }
        public DbSet<Musculo> Musculos { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<EjercicioSesion> EjercicioSesiones { get; set; }
        public DbSet<SesionEntrenamiento> SesionEntrenamientos { get; set; }
        public DbSet<PlanEntrenamiento> PlanEntrenamientos { get; set; }
        public ConfigRutinaDB(DbContextOptions<ConfigRutinaDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // relaciones
            modelBuilder.Entity<Musculo>()
                .HasOne<GrupoMuscular>(m => m.GrupoMuscularEn)
                .WithMany(gm => gm.MusculoLista)
                .HasForeignKey(m => m.IdGrupoMuscular)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ejercicio>(entity => {
                entity.HasOne<CategoriaEjercicio>(e => e.CategoriaEjercicioEn)
                .WithMany(c => c.EjercicioLista)
                .HasForeignKey(e => e.IdCategoriaEjercicio)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Musculo>(e => e.MusculoEn)
                .WithMany(m => m.EjercicioLista)
                .HasForeignKey(e => e.IdMusculo)
                .OnDelete(DeleteBehavior.Restrict);
            });

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

            modelBuilder.Entity<GrupoMuscular>(entity =>
            {
                entity.ToTable("GrupoMuscular");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(50)").IsRequired();
            });

            modelBuilder.Entity<GrupoMuscular>().HasData(
                new GrupoMuscular { Id = 1, Nombre = "Piernas" },
                new GrupoMuscular { Id = 2, Nombre = "Glúteos" },
                new GrupoMuscular { Id = 3, Nombre = "Pantorrillas" },
                new GrupoMuscular { Id = 4, Nombre = "Pecho" },
                new GrupoMuscular { Id = 5, Nombre = "Espalda"},
                new GrupoMuscular { Id = 6, Nombre = "Hombros" },
                new GrupoMuscular { Id = 7, Nombre = "Brazos" },
                new GrupoMuscular { Id = 8, Nombre = "Antebrazos" },
                new GrupoMuscular { Id = 9, Nombre = "Abdomen y Core" },
                new GrupoMuscular { Id = 10, Nombre = "Lumbar" },
                new GrupoMuscular { Id = 11, Nombre = "Cuello y Trapecio" }
                );

            modelBuilder.Entity<Musculo>(entity =>
            {
                entity.ToTable("Musculo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(50)").IsRequired();
            });

            modelBuilder.Entity<Musculo>().HasData(
                // 1. Piernas
                new Musculo { Id = 1, Nombre = "Cuádriceps", IdGrupoMuscular = 1 },
                new Musculo { Id = 2, Nombre = "Isquiotibiales", IdGrupoMuscular = 1 },
                new Musculo { Id = 3, Nombre = "Aductores", IdGrupoMuscular = 1 },
                new Musculo { Id = 4, Nombre = "Abductores", IdGrupoMuscular = 1 },
                // 2. Glúteos
                new Musculo { Id = 5, Nombre = "Glúteo mayor", IdGrupoMuscular = 2 },
                new Musculo { Id = 6, Nombre = "Glúteo medio", IdGrupoMuscular = 2 },
                new Musculo { Id = 7, Nombre = "Glúteo menor", IdGrupoMuscular = 2 },
                // 3. Pantorrillas
                new Musculo { Id = 8, Nombre = "Gemelos", IdGrupoMuscular = 3 },
                new Musculo { Id = 9, Nombre = "Sóleo", IdGrupoMuscular = 3 },
                // 4. Pecho
                new Musculo { Id = 10, Nombre = "Pectoral mayor", IdGrupoMuscular = 4 },
                new Musculo { Id = 11, Nombre = "Pectoral menor", IdGrupoMuscular = 4 },
                // 5. Espalda
                new Musculo { Id = 12, Nombre = "Dorsales", IdGrupoMuscular = 5 },
                new Musculo { Id = 13, Nombre = "Trapecio medio/inferior", IdGrupoMuscular = 5 },
                new Musculo { Id = 14, Nombre = "Romboides", IdGrupoMuscular = 5 },
                // 6. Hombros
                new Musculo { Id = 15, Nombre = "Deltoide anterior", IdGrupoMuscular = 6 },
                new Musculo { Id = 16, Nombre = "Deltoide medio", IdGrupoMuscular = 6 },
                new Musculo { Id = 17, Nombre = "Deltoide posterior", IdGrupoMuscular = 6 },
                new Musculo { Id = 18, Nombre = "Manguito rotador", IdGrupoMuscular = 6 },
                // 7. Brazos
                new Musculo { Id = 19, Nombre = "Bíceps", IdGrupoMuscular = 7 },
                new Musculo { Id = 20, Nombre = "Tríceps", IdGrupoMuscular = 7 },
                // 8. Antebrazos
                new Musculo { Id = 21, Nombre = "Flexores", IdGrupoMuscular = 8 },
                new Musculo { Id = 22, Nombre = "Extensores", IdGrupoMuscular = 8 },
                // 9. Abdomen y Core
                new Musculo { Id = 23, Nombre = "Abdominales superiores", IdGrupoMuscular = 9 },
                new Musculo { Id = 24, Nombre = "Abdominales inferiores", IdGrupoMuscular = 9 },
                new Musculo { Id = 25, Nombre = "Oblicuos", IdGrupoMuscular = 9 },
                new Musculo { Id = 26, Nombre = "Core profundo", IdGrupoMuscular = 9 },
                // 10. Lumbar
                new Musculo { Id = 27, Nombre = "Zona lumbar", IdGrupoMuscular = 10 },
                new Musculo { Id = 28, Nombre = "Erectores", IdGrupoMuscular = 10 },
                // 11. Cuello y trapecio
                new Musculo { Id = 29, Nombre = "Trapecio superior", IdGrupoMuscular = 11 },
                new Musculo { Id = 30, Nombre = "Cuello", IdGrupoMuscular = 11 }
                );
        }
    }
}
