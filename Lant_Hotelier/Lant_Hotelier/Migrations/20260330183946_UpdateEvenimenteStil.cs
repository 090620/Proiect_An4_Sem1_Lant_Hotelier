using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEvenimenteStil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenimente_Hoteluri_ID_Hotel",
                table: "Evenimente");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Evenimente");

            migrationBuilder.RenameColumn(
                name: "Imagine",
                table: "Evenimente",
                newName: "ImagineUrl");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Hotel",
                table: "Evenimente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Evenimente",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DataSiOra",
                table: "Evenimente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenimente_Hoteluri_ID_Hotel",
                table: "Evenimente",
                column: "ID_Hotel",
                principalTable: "Hoteluri",
                principalColumn: "ID_Hotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenimente_Hoteluri_ID_Hotel",
                table: "Evenimente");

            migrationBuilder.DropColumn(
                name: "DataSiOra",
                table: "Evenimente");

            migrationBuilder.RenameColumn(
                name: "ImagineUrl",
                table: "Evenimente",
                newName: "Imagine");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Hotel",
                table: "Evenimente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descriere",
                table: "Evenimente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "Evenimente",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Evenimente_Hoteluri_ID_Hotel",
                table: "Evenimente",
                column: "ID_Hotel",
                principalTable: "Hoteluri",
                principalColumn: "ID_Hotel",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
