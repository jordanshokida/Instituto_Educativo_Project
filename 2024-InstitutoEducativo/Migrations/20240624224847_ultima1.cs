using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2024_InstitutoEducativo.Migrations
{
    /// <inheritdoc />
    public partial class ultima1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Carreras_CarreraId",
                table: "Personas");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Carreras_CarreraId",
                table: "Personas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Carreras_CarreraId",
                table: "Personas");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Carreras_CarreraId",
                table: "Personas",
                column: "CarreraId",
                principalTable: "Carreras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
