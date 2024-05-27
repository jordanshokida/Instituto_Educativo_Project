using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2024_InstitutoEducativo.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaNombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CodMateria = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CupoMaximo = table.Column<int>(type: "int", nullable: false),
                    CarreraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materias_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: true),
                    NumeroMatricula = table.Column<int>(type: "int", nullable: true),
                    CarreraId = table.Column<int>(type: "int", nullable: true),
                    Legajo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profesor_Legajo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: false),
                    Calle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Localidad = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direcciones_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriasCursadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AnioCursada = table.Column<int>(type: "int", nullable: false),
                    Cuatrimestre = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasCursadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasCursadas_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriasCursadas_Personas_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriasCursadas_Personas_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodArea = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calificaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotaFinal = table.Column<int>(type: "int", nullable: false),
                    MateriaCursadaId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calificaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calificaciones_MateriasCursadas_MateriaCursadaId",
                        column: x => x.MateriaCursadaId,
                        principalTable: "MateriasCursadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Calificaciones_Personas_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Calificaciones_Personas_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_AlumnoId",
                table: "Calificaciones",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_MateriaCursadaId",
                table: "Calificaciones",
                column: "MateriaCursadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_MateriaId",
                table: "Calificaciones",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Calificaciones_ProfesorId",
                table: "Calificaciones",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_PersonaId",
                table: "Direcciones",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materias_CarreraId",
                table: "Materias",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasCursadas_AlumnoId",
                table: "MateriasCursadas",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasCursadas_MateriaId",
                table: "MateriasCursadas",
                column: "MateriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MateriasCursadas_ProfesorId",
                table: "MateriasCursadas",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_CarreraId",
                table: "Personas",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_PersonaId",
                table: "Telefonos",
                column: "PersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calificaciones");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "MateriasCursadas");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Carreras");
        }
    }
}
