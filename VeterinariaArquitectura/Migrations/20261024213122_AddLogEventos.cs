using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaArquitectura.Migrations
{
    /// <inheritdoc />
    public partial class AddLogEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEventos",
                columns: table => new
                {
                    LogEventoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proceso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nivel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEventos", x => x.LogEventoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEventos");
        }
    }
}
