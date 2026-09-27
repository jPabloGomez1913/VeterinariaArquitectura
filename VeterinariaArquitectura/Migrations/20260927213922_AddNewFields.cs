using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeterinariaArquitectura.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeroPropietario",
                table: "Mascota",
                newName: "Sexo");

            migrationBuilder.RenameColumn(
                name: "CorreoPropietario",
                table: "Mascota",
                newName: "Raza");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Mascota",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Especie",
                table: "Mascota",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Peso",
                table: "Mascota",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Mascota");

            migrationBuilder.DropColumn(
                name: "Especie",
                table: "Mascota");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Mascota");

            migrationBuilder.RenameColumn(
                name: "Sexo",
                table: "Mascota",
                newName: "NumeroPropietario");

            migrationBuilder.RenameColumn(
                name: "Raza",
                table: "Mascota",
                newName: "CorreoPropietario");
        }
    }
}
