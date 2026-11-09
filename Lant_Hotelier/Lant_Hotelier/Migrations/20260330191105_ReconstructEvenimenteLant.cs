using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class ReconstructEvenimenteLant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenimente_Hoteluri_ID_Hotel",
                table: "Evenimente");

            migrationBuilder.DropIndex(
                name: "IX_Evenimente_ID_Hotel",
                table: "Evenimente");

            migrationBuilder.DropColumn(
                name: "ID_Hotel",
                table: "Evenimente");

            migrationBuilder.AddColumn<int>(
                name: "ID_Lant",
                table: "Evenimente",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Evenimente_ID_Lant",
                table: "Evenimente",
                column: "ID_Lant");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenimente_LanturiHoteliere_ID_Lant",
                table: "Evenimente",
                column: "ID_Lant",
                principalTable: "LanturiHoteliere",
                principalColumn: "ID_Lant",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evenimente_LanturiHoteliere_ID_Lant",
                table: "Evenimente");

            migrationBuilder.DropIndex(
                name: "IX_Evenimente_ID_Lant",
                table: "Evenimente");

            migrationBuilder.DropColumn(
                name: "ID_Lant",
                table: "Evenimente");

            migrationBuilder.AddColumn<int>(
                name: "ID_Hotel",
                table: "Evenimente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evenimente_ID_Hotel",
                table: "Evenimente",
                column: "ID_Hotel");

            migrationBuilder.AddForeignKey(
                name: "FK_Evenimente_Hoteluri_ID_Hotel",
                table: "Evenimente",
                column: "ID_Hotel",
                principalTable: "Hoteluri",
                principalColumn: "ID_Hotel");
        }
    }
}
