using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoGestion.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "SolicitudVacacionAprobacion",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "SolicitudVacacionAprobacion");
        }
    }
}
