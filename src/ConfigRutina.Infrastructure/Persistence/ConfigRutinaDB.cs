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
                new GrupoMuscular { Id = 4, Nombre = "Bíceps" },
                new GrupoMuscular { Id = 5, Nombre = "Tríceps" },
                new GrupoMuscular { Id = 6, Nombre = "Antebrazos Flexor" },
                new GrupoMuscular { Id = 7, Nombre = "Antebrazos Extensor" },
                new GrupoMuscular { Id = 8, Nombre = "Espalda Dorsal y Escapular" },
                new GrupoMuscular { Id = 9, Nombre = "Espalda Lumbar" },
                new GrupoMuscular { Id = 10, Nombre = "Pecho" },
                new GrupoMuscular { Id = 11, Nombre = "Hombros Deltoides" },
                new GrupoMuscular { Id = 12, Nombre = "Hombros Manguito Rotador" },
                new GrupoMuscular { Id = 13, Nombre = "Abdomen y Core" },
                new GrupoMuscular { Id = 14, Nombre = "Zona lumbar" },
                new GrupoMuscular { Id = 15, Nombre = "Cuello y Trapecio Superior" }
                );

            modelBuilder.Entity<Musculo>(entity =>
            {
                entity.ToTable("Musculo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nombre).HasColumnType("VARCHAR(50)").IsRequired();
            });

            modelBuilder.Entity<Musculo>().HasData(
                // Piernas
                new Musculo { Id = 1, Nombre = "Cuádriceps recto femoral", IdGrupoMuscular = 1 },
                new Musculo { Id = 2, Nombre = "Cuádriceps vasto lateral", IdGrupoMuscular = 1 },
                new Musculo { Id = 3, Nombre = "Cuádriceps vasto medial", IdGrupoMuscular = 1 },
                new Musculo { Id = 4, Nombre = "Cuádriceps vasto intermedio", IdGrupoMuscular = 1 },
                new Musculo { Id = 5, Nombre = "Isquiotibiales bíceps femoral", IdGrupoMuscular = 1 },
                new Musculo { Id = 6, Nombre = "Isquiotibiales semitendinoso", IdGrupoMuscular = 1 },
                new Musculo { Id = 7, Nombre = "Isquiotibiales semimembranoso", IdGrupoMuscular = 1 },
                new Musculo { Id = 8, Nombre = "Aductores mayor", IdGrupoMuscular = 1 },
                new Musculo { Id = 9, Nombre = "Aductores largo", IdGrupoMuscular = 1 },
                new Musculo { Id = 10, Nombre = "Aductores corto", IdGrupoMuscular = 1 },
                new Musculo { Id = 11, Nombre = "Aductores pectíneo", IdGrupoMuscular = 1 },
                new Musculo { Id = 12, Nombre = "Aductores grácil", IdGrupoMuscular = 1 },
                new Musculo { Id = 13, Nombre = "Abductores", IdGrupoMuscular = 1 },
                // Glúteos
                new Musculo { Id = 14, Nombre = "Glúteo mayor", IdGrupoMuscular = 2 },
                new Musculo { Id = 15, Nombre = "Glúteo medio", IdGrupoMuscular = 2 },
                new Musculo { Id = 16, Nombre = "Glúteo menor", IdGrupoMuscular = 2 },
                // Pantorrillas
                new Musculo { Id = 17, Nombre = "Gastrocnemio (gemelo)", IdGrupoMuscular = 3 },
                new Musculo { Id = 18, Nombre = "Sóleo", IdGrupoMuscular = 3 },
                new Musculo { Id = 19, Nombre = "Tibial posterior", IdGrupoMuscular = 3 },
                new Musculo { Id = 20, Nombre = "Plantar", IdGrupoMuscular = 3 },
                new Musculo { Id = 21, Nombre = "Peroneo largo", IdGrupoMuscular = 3 },
                new Musculo { Id = 22, Nombre = "Peroneo corto", IdGrupoMuscular = 3 },
                // Bíceps
                new Musculo { Id = 23, Nombre = "Bíceps braquial cabeza larga", IdGrupoMuscular = 4 },
                new Musculo { Id = 24, Nombre = "Bíceps braquial cabeza corta", IdGrupoMuscular = 4 },
                new Musculo { Id = 25, Nombre = "Braquial anterior", IdGrupoMuscular = 4 },
                new Musculo { Id = 26, Nombre = "Braquiorradial", IdGrupoMuscular = 4 },
                // Tríceps
                new Musculo { Id = 27, Nombre = "Tríceps braquial cabeza larga", IdGrupoMuscular = 5 },
                new Musculo { Id = 28, Nombre = "Tríceps braquial cabeza lateral", IdGrupoMuscular = 5 },
                new Musculo { Id = 29, Nombre = "Tríceps braquial cabeza medial", IdGrupoMuscular = 5 },
                // Antebrazos Flexor
                new Musculo { Id = 30, Nombre = "Flexor radial del carpo", IdGrupoMuscular = 6 },
                new Musculo { Id = 31, Nombre = "Flexor cubital del carpo", IdGrupoMuscular = 6 },
                new Musculo { Id = 32, Nombre = "Flexor superficial de los dedos", IdGrupoMuscular = 6 },
                new Musculo { Id = 33, Nombre = "Flexor profundo de los dedos", IdGrupoMuscular = 6 },
                new Musculo { Id = 34, Nombre = "Flexor largo del pulgar", IdGrupoMuscular = 6 },
                new Musculo { Id = 35, Nombre = "Palmar largo", IdGrupoMuscular = 6 },
                new Musculo { Id = 36, Nombre = "Pronador redondo", IdGrupoMuscular = 6 },
                new Musculo { Id = 37, Nombre = "Pronador cuadrado", IdGrupoMuscular = 6 },
                // Antebrazos Extensor
                new Musculo { Id = 38, Nombre = "Extensor radial largo del carpo", IdGrupoMuscular = 7 },
                new Musculo { Id = 39, Nombre = "Extensor radial corto del carpo", IdGrupoMuscular = 7 },
                new Musculo { Id = 40, Nombre = "Extensor cubital del carpo", IdGrupoMuscular = 7 },
                new Musculo { Id = 41, Nombre = "Extensor común de los dedos", IdGrupoMuscular = 7 },
                new Musculo { Id = 42, Nombre = "Extensor del meñique", IdGrupoMuscular = 7 },
                new Musculo { Id = 43, Nombre = "Extensor largo del pulgar", IdGrupoMuscular = 7 },
                new Musculo { Id = 44, Nombre = "Extensor corto del pulgar", IdGrupoMuscular = 7 },
                new Musculo { Id = 45, Nombre = "Extensor del índice", IdGrupoMuscular = 7 },
                new Musculo { Id = 46, Nombre = "Supinador", IdGrupoMuscular = 7 },
                // Espalda Dorsal y Escapular
                new Musculo { Id = 47, Nombre = "Dorsal ancho", IdGrupoMuscular = 8 },
                new Musculo { Id = 48, Nombre = "Trapecio superior", IdGrupoMuscular = 8 },
                new Musculo { Id = 49, Nombre = "Trapecio medio", IdGrupoMuscular = 8 },
                new Musculo { Id = 50, Nombre = "Trapecio inferior", IdGrupoMuscular = 8 },
                new Musculo { Id = 51, Nombre = "Romboide mayor", IdGrupoMuscular = 8 },
                new Musculo { Id = 52, Nombre = "Romboide menor", IdGrupoMuscular = 8 },
                new Musculo { Id = 53, Nombre = "Redondo mayor", IdGrupoMuscular = 8 },
                // Espalda Lumbar
                new Musculo { Id = 54, Nombre = "Erectores espinales iliocostal", IdGrupoMuscular = 9 },
                new Musculo { Id = 55, Nombre = "Erectores espinales longísimo", IdGrupoMuscular = 9 },
                new Musculo { Id = 56, Nombre = "Erectores espinales espinoso", IdGrupoMuscular = 9 },
                new Musculo { Id = 57, Nombre = "Multífidos", IdGrupoMuscular = 9 },
                new Musculo { Id = 58, Nombre = "Cuadrado lumbar", IdGrupoMuscular = 9 },
                // Pecho
                new Musculo { Id = 59, Nombre = "Pectoral mayor porción clavicular", IdGrupoMuscular = 10 },
                new Musculo { Id = 60, Nombre = "Pectoral mayor esternal", IdGrupoMuscular = 10 },
                new Musculo { Id = 61, Nombre = "Pectoral mayor abdominal", IdGrupoMuscular = 10 },
                new Musculo { Id = 62, Nombre = "Pectoral menor", IdGrupoMuscular = 10 },
                new Musculo { Id = 63, Nombre = "Serrato anterior", IdGrupoMuscular = 10 },
                new Musculo { Id = 64, Nombre = "Subclavio", IdGrupoMuscular = 10 },
                // Hombros Deltoides
                new Musculo { Id = 65, Nombre = "Deltoide anterior", IdGrupoMuscular = 11 },
                new Musculo { Id = 66, Nombre = "Deltoide lateral", IdGrupoMuscular = 11 },
                new Musculo { Id = 67, Nombre = "Deltoide posterior", IdGrupoMuscular = 11 },
                // Hombros Manguito Rotador
                new Musculo { Id = 68, Nombre = "Supraespinoso", IdGrupoMuscular = 12 },
                new Musculo { Id = 69, Nombre = "Infraespinoso", IdGrupoMuscular = 12 },
                new Musculo { Id = 70, Nombre = "Subescapular", IdGrupoMuscular = 12 },
                new Musculo { Id = 71, Nombre = "Redondo menor", IdGrupoMuscular = 12 },
                // Abdomen y Core
                new Musculo { Id = 72, Nombre = "Recto abdominal", IdGrupoMuscular = 13 },
                new Musculo { Id = 73, Nombre = "Oblicuo externo", IdGrupoMuscular = 13 },
                new Musculo { Id = 74, Nombre = "Oblicuo interno", IdGrupoMuscular = 13 },
                new Musculo { Id = 75, Nombre = "Transverso abdominal", IdGrupoMuscular = 13 },
                new Musculo { Id = 76, Nombre = "Diafragma", IdGrupoMuscular = 13 },
                // Zona lumbar
                new Musculo { Id = 77, Nombre = "Erectores espinales", IdGrupoMuscular = 14 },
                new Musculo { Id = 78, Nombre = "Multífidos", IdGrupoMuscular = 14 },
                new Musculo { Id = 79, Nombre = "Cuadrado lumbar", IdGrupoMuscular = 14 },
                // Cuello y trapecio superior
                new Musculo { Id = 80, Nombre = "Trapecio superior", IdGrupoMuscular = 15 },
                new Musculo { Id = 81, Nombre = "Esternocleidomastoideo", IdGrupoMuscular = 15 },
                new Musculo { Id = 82, Nombre = "Escalenos", IdGrupoMuscular = 15 },
                new Musculo { Id = 83, Nombre = "Esplenio del cuello", IdGrupoMuscular = 15 }
                );
        }
    }
}
