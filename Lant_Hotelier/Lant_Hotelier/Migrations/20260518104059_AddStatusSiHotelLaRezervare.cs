using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusSiHotelLaRezervare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Rezervari",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCreare",
                table: "Rezervari",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ID_Hotel",
                table: "Rezervari",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observatii",
                table: "Rezervari",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PretTotal",
                table: "Rezervari",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rezervari",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ID_Hotel",
                table: "Rezervari",
                column: "ID_Hotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Rezervari_Hoteluri_ID_Hotel",
                table: "Rezervari",
                column: "ID_Hotel",
                principalTable: "Hoteluri",
                principalColumn: "ID_Hotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rezervari_Hoteluri_ID_Hotel",
                table: "Rezervari");

            migrationBuilder.DropIndex(
                name: "IX_Rezervari_ID_Hotel",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "DataCreare",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "ID_Hotel",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "Observatii",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "PretTotal",
                table: "Rezervari");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rezervari");
        }
    }
}
