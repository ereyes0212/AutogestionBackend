using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoGestion.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoTipoSolicitud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampoTipoSolicitud",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoSolicitudId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoCampo = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nivel = table.Column<int>(type: "int", nullable: false),
                    EsRequerido = table.Column<ulong>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_CampoTipoSolicitud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampoTipoSolicitud_TipoSolicitud_TipoSolicitudId",
                        column: x => x.TipoSolicitudId,
                        principalTable: "TipoSolicitud",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CampoTipoSolicitud_TipoSolicitudId",
                table: "CampoTipoSolicitud",
                column: "TipoSolicitudId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampoTipoSolicitud");
        }
    }
}
