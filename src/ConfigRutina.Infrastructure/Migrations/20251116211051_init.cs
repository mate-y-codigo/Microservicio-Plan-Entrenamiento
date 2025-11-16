using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConfigRutina.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    { 4, "Pecho" },
                    { 5, "Espalda" },
                    { 6, "Hombros" },
                    { 7, "Brazos" },
                    { 8, "Antebrazos" },
                    { 9, "Abdomen y Core" },
                    { 10, "Lumbar" },
                    { 11, "Cuello y Trapecio" }
                });

            migrationBuilder.InsertData(
                table: "Musculo",
                columns: new[] { "Id", "IdGrupoMuscular", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Cuádriceps" },
                    { 2, 1, "Isquiotibiales" },
                    { 3, 1, "Aductores" },
                    { 4, 1, "Abductores" },
                    { 5, 2, "Glúteo mayor" },
                    { 6, 2, "Glúteo medio" },
                    { 7, 2, "Glúteo menor" },
                    { 8, 3, "Gemelos" },
                    { 9, 3, "Sóleo" },
                    { 10, 4, "Pectoral mayor" },
                    { 11, 4, "Pectoral menor" },
                    { 12, 5, "Dorsales" },
                    { 13, 5, "Trapecio medio/inferior" },
                    { 14, 5, "Romboides" },
                    { 15, 6, "Deltoide anterior" },
                    { 16, 6, "Deltoide medio" },
                    { 17, 6, "Deltoide posterior" },
                    { 18, 6, "Manguito rotador" },
                    { 19, 7, "Bíceps" },
                    { 20, 7, "Tríceps" },
                    { 21, 8, "Flexores" },
                    { 22, 8, "Extensores" },
                    { 23, 9, "Abdominales superiores" },
                    { 24, 9, "Abdominales inferiores" },
                    { 25, 9, "Oblicuos" },
                    { 26, 9, "Core profundo" },
                    { 27, 10, "Zona lumbar" },
                    { 28, 10, "Erectores" },
                    { 29, 11, "Trapecio superior" },
                    { 30, 11, "Cuello" }
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
