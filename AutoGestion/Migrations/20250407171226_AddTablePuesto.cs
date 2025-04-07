using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoGestion.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "jefe_id",
                table: "Empleados",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "puesto_id",
                table: "Empleados",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Puesto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Empresa_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<ulong>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Adicionado_por = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificado_por = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Puesto_Empresa_Empresa_id",
                        column: x => x.Empresa_id,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_jefe_id",
                table: "Empleados",
                column: "jefe_id");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_puesto_id",
                table: "Empleados",
                column: "puesto_id");

            migrationBuilder.CreateIndex(
                name: "IX_Puesto_Empresa_id",
                table: "Puesto",
                column: "Empresa_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Empleados_jefe_id",
                table: "Empleados",
                column: "jefe_id",
                principalTable: "Empleados",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Puesto_puesto_id",
                table: "Empleados",
                column: "puesto_id",
                principalTable: "Puesto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Empleados_jefe_id",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Puesto_puesto_id",
                table: "Empleados");

            migrationBuilder.DropTable(
                name: "Puesto");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_jefe_id",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_puesto_id",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "jefe_id",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "puesto_id",
                table: "Empleados");
        }
    }
}
