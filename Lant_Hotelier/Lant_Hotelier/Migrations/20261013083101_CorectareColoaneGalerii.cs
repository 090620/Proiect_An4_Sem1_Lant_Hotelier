using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class CorectareColoaneGalerii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Imagine",
                table: "Galerii",
                newName: "ID_Galerie");

            migrationBuilder.RenameColumn(
                name: "URL_Imagine",
                table: "Galerii",
                newName: "CaleImagine");

            migrationBuilder.AddColumn<string>(
                name: "NumeImagine",
                table: "Galerii",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeImagine",
                table: "Galerii");

            migrationBuilder.RenameColumn(
                name: "CaleImagine",
                table: "Galerii",
                newName: "URL_Imagine");

            migrationBuilder.RenameColumn(
                name: "ID_Galerie",
                table: "Galerii",
                newName: "ID_Imagine");
        }
    }
}