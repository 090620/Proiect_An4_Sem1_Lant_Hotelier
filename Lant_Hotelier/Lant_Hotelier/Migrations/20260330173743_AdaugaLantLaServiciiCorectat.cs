using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class AdaugaLantLaServiciiCorectat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Iconita",
                table: "Servicii",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Servicii",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ID_Lant",
                table: "Servicii",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicii_ID_Lant",
                table: "Servicii",
                column: "ID_Lant");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicii_LanturiHoteliere_ID_Lant",
                table: "Servicii",
                column: "ID_Lant",
                principalTable: "LanturiHoteliere",
                principalColumn: "ID_Lant",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicii_LanturiHoteliere_ID_Lant",
                table: "Servicii");

            migrationBuilder.DropIndex(
                name: "IX_Servicii_ID_Lant",
                table: "Servicii");

            migrationBuilder.DropColumn(
                name: "ID_Lant",
                table: "Servicii");

            migrationBuilder.AlterColumn<string>(
                name: "Iconita",
                table: "Servicii",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Servicii",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
