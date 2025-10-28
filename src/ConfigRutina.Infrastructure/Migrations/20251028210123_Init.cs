using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConfigRutina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaEjercicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "VARCHAR(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaEjercicio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupoMuscular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoMuscular", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanEntrenamiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    EsPlantilla = table.Column<bool>(type: "BOOL", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                    Activo = table.Column<bool>(type: "BOOL", nullable: false),
                    IdEntrenador = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanEntrenamiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    IdGrupoMuscular = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musculo_GrupoMuscular_IdGrupoMuscular",
                        column: x => x.IdGrupoMuscular,
                        principalTable: "GrupoMuscular",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SesionEntrenamiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Orden = table.Column<int>(type: "INT", nullable: false),
                    IdPlanEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionEntrenamiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionEntrenamiento_PlanEntrenamiento_IdPlanEntrenamiento",
                        column: x => x.IdPlanEntrenamiento,
                        principalTable: "PlanEntrenamiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ejercicio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    UrlDemo = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "BOOL", nullable: false),
                    IdCategoriaEjercicio = table.Column<int>(type: "integer", nullable: false),
                    IdMusculo = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejercicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ejercicio_CategoriaEjercicio_IdCategoriaEjercicio",
                        column: x => x.IdCategoriaEjercicio,
                        principalTable: "CategoriaEjercicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ejercicio_Musculo_IdMusculo",
                        column: x => x.IdMusculo,
                        principalTable: "Musculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EjercicioSesion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeriesObjetivo = table.Column<int>(type: "INT", nullable: false),
                    RepeticionesObjetivo = table.Column<int>(type: "INT", nullable: false),
                    PesoObjetivo = table.Column<float>(type: "FLOAT", nullable: false),
                    Descanso = table.Column<int>(type: "INT", nullable: false),
                    Orden = table.Column<int>(type: "INT", nullable: false),
                    IdSesionEntrenamiento = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEjercicio = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EjercicioSesion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EjercicioSesion_Ejercicio_IdEjercicio",
                        column: x => x.IdEjercicio,
                        principalTable: "Ejercicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EjercicioSesion_SesionEntrenamiento_IdSesionEntrenamiento",
                        column: x => x.IdSesionEntrenamiento,
                        principalTable: "SesionEntrenamiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CategoriaEjercicio",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Movilidad" },
                    { 2, "Calentamiento" },
                    { 3, "Fuerza" },
                    { 4, "Hipertrofia" }
                });

            migrationBuilder.InsertData(
                table: "GrupoMuscular",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Piernas" },
                    { 2, "Glúteos" },
                    { 3, "Pantorrillas" },
                    { 4, "Bíceps" },
                    { 5, "Tríceps" },
                    { 6, "Antebrazos Flexor" },
                    { 7, "Antebrazos Extensor" },
                    { 8, "Espalda Dorsal y Escapular" },
                    { 9, "Espalda Lumbar" },
                    { 10, "Pecho" },
                    { 11, "Hombros Deltoides" },
                    { 12, "Hombros Manguito Rotador" },
                    { 13, "Abdomen y Core" },
                    { 14, "Zona lumbar" },
                    { 15, "Cuello y Trapecio Superior" }
                });

            migrationBuilder.InsertData(
                table: "Musculo",
                columns: new[] { "Id", "IdGrupoMuscular", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Cuádriceps recto femoral" },
                    { 2, 1, "Cuádriceps vasto lateral" },
                    { 3, 1, "Cuádriceps vasto medial" },
                    { 4, 1, "Cuádriceps vasto intermedio" },
                    { 5, 1, "Isquiotibiales bíceps femoral" },
                    { 6, 1, "Isquiotibiales semitendinoso" },
                    { 7, 1, "Isquiotibiales semimembranoso" },
                    { 8, 1, "Aductores mayor" },
                    { 9, 1, "Aductores largo" },
                    { 10, 1, "Aductores corto" },
                    { 11, 1, "Aductores pectíneo" },
                    { 12, 1, "Aductores grácil" },
                    { 13, 1, "Abductores" },
                    { 14, 2, "Glúteo mayor" },
                    { 15, 2, "Glúteo medio" },
                    { 16, 2, "Glúteo menor" },
                    { 17, 3, "Gastrocnemio (gemelo)" },
                    { 18, 3, "Sóleo" },
                    { 19, 3, "Tibial posterior" },
                    { 20, 3, "Plantar" },
                    { 21, 3, "Peroneo largo" },
                    { 22, 3, "Peroneo corto" },
                    { 23, 4, "Bíceps braquial cabeza larga" },
                    { 24, 4, "Bíceps braquial cabeza corta" },
                    { 25, 4, "Braquial anterior" },
                    { 26, 4, "Braquiorradial" },
                    { 27, 5, "Tríceps braquial cabeza larga" },
                    { 28, 5, "Tríceps braquial cabeza lateral" },
                    { 29, 5, "Tríceps braquial cabeza medial" },
                    { 30, 6, "Flexor radial del carpo" },
                    { 31, 6, "Flexor cubital del carpo" },
                    { 32, 6, "Flexor superficial de los dedos" },
                    { 33, 6, "Flexor profundo de los dedos" },
                    { 34, 6, "Flexor largo del pulgar" },
                    { 35, 6, "Palmar largo" },
                    { 36, 6, "Pronador redondo" },
                    { 37, 6, "Pronador cuadrado" },
                    { 38, 7, "Extensor radial largo del carpo" },
                    { 39, 7, "Extensor radial corto del carpo" },
                    { 40, 7, "Extensor cubital del carpo" },
                    { 41, 7, "Extensor común de los dedos" },
                    { 42, 7, "Extensor del meñique" },
                    { 43, 7, "Extensor largo del pulgar" },
                    { 44, 7, "Extensor corto del pulgar" },
                    { 45, 7, "Extensor del índice" },
                    { 46, 7, "Supinador" },
                    { 47, 8, "Dorsal ancho" },
                    { 48, 8, "Trapecio superior" },
                    { 49, 8, "Trapecio medio" },
                    { 50, 8, "Trapecio inferior" },
                    { 51, 8, "Romboide mayor" },
                    { 52, 8, "Romboide menor" },
                    { 53, 8, "Redondo mayor" },
                    { 54, 9, "Erectores espinales iliocostal" },
                    { 55, 9, "Erectores espinales longísimo" },
                    { 56, 9, "Erectores espinales espinoso" },
                    { 57, 9, "Multífidos" },
                    { 58, 9, "Cuadrado lumbar" },
                    { 59, 10, "Pectoral mayor porción clavicular" },
                    { 60, 10, "Pectoral mayor esternal" },
                    { 61, 10, "Pectoral mayor abdominal" },
                    { 62, 10, "Pectoral menor" },
                    { 63, 10, "Serrato anterior" },
                    { 64, 10, "Subclavio" },
                    { 65, 11, "Deltoide anterior" },
                    { 66, 11, "Deltoide lateral" },
                    { 67, 11, "Deltoide posterior" },
                    { 68, 12, "Supraespinoso" },
                    { 69, 12, "Infraespinoso" },
                    { 70, 12, "Subescapular" },
                    { 71, 12, "Redondo menor" },
                    { 72, 13, "Recto abdominal" },
                    { 73, 13, "Oblicuo externo" },
                    { 74, 13, "Oblicuo interno" },
                    { 75, 13, "Transverso abdominal" },
                    { 76, 13, "Diafragma" },
                    { 77, 14, "Erectores espinales" },
                    { 78, 14, "Multífidos" },
                    { 79, 14, "Cuadrado lumbar" },
                    { 80, 15, "Trapecio superior" },
                    { 81, 15, "Esternocleidomastoideo" },
                    { 82, 15, "Escalenos" },
                    { 83, 15, "Esplenio del cuello" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicio_IdCategoriaEjercicio",
                table: "Ejercicio",
                column: "IdCategoriaEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicio_IdMusculo",
                table: "Ejercicio",
                column: "IdMusculo");

            migrationBuilder.CreateIndex(
                name: "IX_EjercicioSesion_IdEjercicio",
                table: "EjercicioSesion",
                column: "IdEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_EjercicioSesion_IdSesionEntrenamiento",
                table: "EjercicioSesion",
                column: "IdSesionEntrenamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Musculo_IdGrupoMuscular",
                table: "Musculo",
                column: "IdGrupoMuscular");

            migrationBuilder.CreateIndex(
                name: "IX_SesionEntrenamiento_IdPlanEntrenamiento",
                table: "SesionEntrenamiento",
                column: "IdPlanEntrenamiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EjercicioSesion");

            migrationBuilder.DropTable(
                name: "Ejercicio");

            migrationBuilder.DropTable(
                name: "SesionEntrenamiento");

            migrationBuilder.DropTable(
                name: "CategoriaEjercicio");

            migrationBuilder.DropTable(
                name: "Musculo");

            migrationBuilder.DropTable(
                name: "PlanEntrenamiento");

            migrationBuilder.DropTable(
                name: "GrupoMuscular");
        }
    }
}
