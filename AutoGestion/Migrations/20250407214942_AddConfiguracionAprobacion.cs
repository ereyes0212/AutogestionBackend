using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoGestion.Migrations
{
    /// <inheritdoc />
    public partial class AddConfiguracionAprobacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Puesto_puesto_id",
                table: "Empleados");

            migrationBuilder.UpdateData(
                table: "Empleados",
                keyColumn: "puesto_id",
                keyValue: null,
                column: "puesto_id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "puesto_id",
                table: "Empleados",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConfiguracionAprobacion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Empresa_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    puesto_id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nivel = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<ulong>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Adicionado_por = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Modificado_por = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionAprobacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfiguracionAprobacion_Empresa_Empresa_id",
                        column: x => x.Empresa_id,
                        principalTable: "Empresa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConfiguracionAprobacion_Puesto_puesto_id",
                        column: x => x.puesto_id,
                        principalTable: "Puesto",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionAprobacion_Empresa_id",
                table: "ConfiguracionAprobacion",
                column: "Empresa_id");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionAprobacion_puesto_id",
                table: "ConfiguracionAprobacion",
                column: "puesto_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Puesto_puesto_id",
                table: "Empleados",
                column: "puesto_id",
                principalTable: "Puesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Puesto_puesto_id",
                table: "Empleados");

            migrationBuilder.DropTable(
                name: "ConfiguracionAprobacion");

            migrationBuilder.AlterColumn<string>(
                name: "puesto_id",
                table: "Empleados",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Puesto_puesto_id",
                table: "Empleados",
                column: "puesto_id",
                principalTable: "Puesto",
                principalColumn: "Id");
        }
    }
}
