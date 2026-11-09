using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class AdaugaPretLaServicii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hoteluri_Servicii");

            migrationBuilder.AddColumn<decimal>(
                name: "Pret",
                table: "Servicii",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "HotelServicii",
                columns: table => new
                {
                    ID_Hotel = table.Column<int>(type: "int", nullable: false),
                    ID_Servicii = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelServicii", x => new { x.ID_Hotel, x.ID_Servicii });
                    table.ForeignKey(
                        name: "FK_HotelServicii_Hoteluri_ID_Hotel",
                        column: x => x.ID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelServicii_Servicii_ID_Servicii",
                        column: x => x.ID_Servicii,
                        principalTable: "Servicii",
                        principalColumn: "ID_Servicii",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HotelServicii_ID_Servicii",
                table: "HotelServicii",
                column: "ID_Servicii");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HotelServicii");

            migrationBuilder.DropColumn(
                name: "Pret",
                table: "Servicii");

            migrationBuilder.CreateTable(
                name: "Hoteluri_Servicii",
                columns: table => new
                {
                    ID_Hotel = table.Column<int>(type: "int", nullable: false),
                    ID_Servicii = table.Column<int>(type: "int", nullable: false),
                    HotelID_Hotel = table.Column<int>(type: "int", nullable: false),
                    ServiciiID_Servicii = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteluri_Servicii", x => new { x.ID_Hotel, x.ID_Servicii });
                    table.ForeignKey(
                        name: "FK_Hoteluri_Servicii_Hoteluri_HotelID_Hotel",
                        column: x => x.HotelID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hoteluri_Servicii_Servicii_ServiciiID_Servicii",
                        column: x => x.ServiciiID_Servicii,
                        principalTable: "Servicii",
                        principalColumn: "ID_Servicii",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_Servicii_HotelID_Hotel",
                table: "Hoteluri_Servicii",
                column: "HotelID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_Servicii_ServiciiID_Servicii",
                table: "Hoteluri_Servicii",
                column: "ServiciiID_Servicii");
        }
    }
}
