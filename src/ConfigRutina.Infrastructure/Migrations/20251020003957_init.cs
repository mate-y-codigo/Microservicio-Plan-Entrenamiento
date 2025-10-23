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
                name: "Ejercicio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    MusculoPrincipal = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    GrupoMuscular = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    UrlDemo = table.Column<string>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "BOOL", nullable: false),
                    IdCategoriaEjercicio = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Ejercicio_IdCategoriaEjercicio",
                table: "Ejercicio",
                column: "IdCategoriaEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_EjercicioSesion_IdEjercicio",
                table: "EjercicioSesion",
                column: "IdEjercicio");

            migrationBuilder.CreateIndex(
                name: "IX_EjercicioSesion_IdSesionEntrenamiento",
                table: "EjercicioSesion",
                column: "IdSesionEntrenamiento");

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
                name: "PlanEntrenamiento");
        }
    }
}
