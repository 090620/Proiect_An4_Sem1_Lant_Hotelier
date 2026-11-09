using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOferteDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cod_Promo",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "Data_Final",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "Data_Start",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "Descriere_Oferta",
                table: "Oferte");

            migrationBuilder.RenameColumn(
                name: "Titlu_Oferta",
                table: "Oferte",
                newName: "Titlu");

            migrationBuilder.RenameColumn(
                name: "Procent_Reducere",
                table: "Oferte",
                newName: "ProcentReducere");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Lant",
                table: "Servicii",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CodPromotional",
                table: "Oferte",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descriere",
                table: "Oferte",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ID_Lant",
                table: "Oferte",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LantHotelierID_Lant",
                table: "Oferte",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PretNou",
                table: "Oferte",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PretVechi",
                table: "Oferte",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Oferte_LantHotelierID_Lant",
                table: "Oferte",
                column: "LantHotelierID_Lant");

            migrationBuilder.AddForeignKey(
                name: "FK_Oferte_LanturiHoteliere_LantHotelierID_Lant",
                table: "Oferte",
                column: "LantHotelierID_Lant",
                principalTable: "LanturiHoteliere",
                principalColumn: "ID_Lant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oferte_LanturiHoteliere_LantHotelierID_Lant",
                table: "Oferte");

            migrationBuilder.DropIndex(
                name: "IX_Oferte_LantHotelierID_Lant",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "CodPromotional",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "Descriere",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "ID_Lant",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "LantHotelierID_Lant",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "PretNou",
                table: "Oferte");

            migrationBuilder.DropColumn(
                name: "PretVechi",
                table: "Oferte");

            migrationBuilder.RenameColumn(
                name: "Titlu",
                table: "Oferte",
                newName: "Titlu_Oferta");

            migrationBuilder.RenameColumn(
                name: "ProcentReducere",
                table: "Oferte",
                newName: "Procent_Reducere");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Lant",
                table: "Servicii",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cod_Promo",
                table: "Oferte",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Final",
                table: "Oferte",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_Start",
                table: "Oferte",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descriere_Oferta",
                table: "Oferte",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
