using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lant_Hotelier.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanturiHoteliere",
                columns: table => new
                {
                    ID_Lant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume_Lant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanturiHoteliere", x => x.ID_Lant);
                });

            migrationBuilder.CreateTable(
                name: "Oferte",
                columns: table => new
                {
                    ID_Oferta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu_Oferta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Procent_Reducere = table.Column<int>(type: "int", nullable: false),
                    Descriere_Oferta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cod_Promo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data_Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Final = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oferte", x => x.ID_Oferta);
                });

            migrationBuilder.CreateTable(
                name: "Servicii",
                columns: table => new
                {
                    ID_Servicii = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iconita = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicii", x => x.ID_Servicii);
                });

            migrationBuilder.CreateTable(
                name: "Utilizatori",
                columns: table => new
                {
                    ID_Utilizator = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol_Utilizator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizatori", x => x.ID_Utilizator);
                });

            migrationBuilder.CreateTable(
                name: "Camere",
                columns: table => new
                {
                    ID_Camera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip_Camera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pret = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Lant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Camere", x => x.ID_Camera);
                    table.ForeignKey(
                        name: "FK_Camere_LanturiHoteliere_ID_Lant",
                        column: x => x.ID_Lant,
                        principalTable: "LanturiHoteliere",
                        principalColumn: "ID_Lant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hoteluri",
                columns: table => new
                {
                    ID_Hotel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Lant = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteluri", x => x.ID_Hotel);
                    table.ForeignKey(
                        name: "FK_Hoteluri_LanturiHoteliere_ID_Lant",
                        column: x => x.ID_Lant,
                        principalTable: "LanturiHoteliere",
                        principalColumn: "ID_Lant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervari",
                columns: table => new
                {
                    ID_Rezervare = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_Inceput = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Sfarsit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tip_Camera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Camera = table.Column<int>(type: "int", nullable: false),
                    ID_Utilizator = table.Column<int>(type: "int", nullable: false),
                    ID_Oferta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervari", x => x.ID_Rezervare);
                    table.ForeignKey(
                        name: "FK_Rezervari_Camere_ID_Camera",
                        column: x => x.ID_Camera,
                        principalTable: "Camere",
                        principalColumn: "ID_Camera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervari_Oferte_ID_Oferta",
                        column: x => x.ID_Oferta,
                        principalTable: "Oferte",
                        principalColumn: "ID_Oferta");
                    table.ForeignKey(
                        name: "FK_Rezervari_Utilizatori_ID_Utilizator",
                        column: x => x.ID_Utilizator,
                        principalTable: "Utilizatori",
                        principalColumn: "ID_Utilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacte",
                columns: table => new
                {
                    ID_Contact = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Hotel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacte", x => x.ID_Contact);
                    table.ForeignKey(
                        name: "FK_Contacte_Hoteluri_ID_Hotel",
                        column: x => x.ID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evenimente",
                columns: table => new
                {
                    ID_Eveniment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titlu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Imagine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Hotel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evenimente", x => x.ID_Eveniment);
                    table.ForeignKey(
                        name: "FK_Evenimente_Hoteluri_ID_Hotel",
                        column: x => x.ID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Galerii",
                columns: table => new
                {
                    ID_Imagine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL_Imagine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Hotel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galerii", x => x.ID_Imagine);
                    table.ForeignKey(
                        name: "FK_Galerii_Hoteluri_ID_Hotel",
                        column: x => x.ID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hoteluri_Oferte",
                columns: table => new
                {
                    ID_Hotel = table.Column<int>(type: "int", nullable: false),
                    ID_Oferta = table.Column<int>(type: "int", nullable: false),
                    HotelID_Hotel = table.Column<int>(type: "int", nullable: false),
                    OferteID_Oferta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteluri_Oferte", x => new { x.ID_Hotel, x.ID_Oferta });
                    table.ForeignKey(
                        name: "FK_Hoteluri_Oferte_Hoteluri_HotelID_Hotel",
                        column: x => x.HotelID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hoteluri_Oferte_Oferte_OferteID_Oferta",
                        column: x => x.OferteID_Oferta,
                        principalTable: "Oferte",
                        principalColumn: "ID_Oferta",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Recenzii",
                columns: table => new
                {
                    ID_Recenzie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume_Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentariu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ID_Hotel = table.Column<int>(type: "int", nullable: false),
                    ID_Client = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recenzii", x => x.ID_Recenzie);
                    table.ForeignKey(
                        name: "FK_Recenzii_Hoteluri_ID_Hotel",
                        column: x => x.ID_Hotel,
                        principalTable: "Hoteluri",
                        principalColumn: "ID_Hotel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recenzii_Utilizatori_ID_Client",
                        column: x => x.ID_Client,
                        principalTable: "Utilizatori",
                        principalColumn: "ID_Utilizator",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Camere_ID_Lant",
                table: "Camere",
                column: "ID_Lant");

            migrationBuilder.CreateIndex(
                name: "IX_Contacte_ID_Hotel",
                table: "Contacte",
                column: "ID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Evenimente_ID_Hotel",
                table: "Evenimente",
                column: "ID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Galerii_ID_Hotel",
                table: "Galerii",
                column: "ID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_ID_Lant",
                table: "Hoteluri",
                column: "ID_Lant");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_Oferte_HotelID_Hotel",
                table: "Hoteluri_Oferte",
                column: "HotelID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_Oferte_OferteID_Oferta",
                table: "Hoteluri_Oferte",
                column: "OferteID_Oferta");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_Servicii_HotelID_Hotel",
                table: "Hoteluri_Servicii",
                column: "HotelID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Hoteluri_Servicii_ServiciiID_Servicii",
                table: "Hoteluri_Servicii",
                column: "ServiciiID_Servicii");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_ID_Client",
                table: "Recenzii",
                column: "ID_Client");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzii_ID_Hotel",
                table: "Recenzii",
                column: "ID_Hotel");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ID_Camera",
                table: "Rezervari",
                column: "ID_Camera");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ID_Oferta",
                table: "Rezervari",
                column: "ID_Oferta");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervari_ID_Utilizator",
                table: "Rezervari",
                column: "ID_Utilizator");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacte");

            migrationBuilder.DropTable(
                name: "Evenimente");

            migrationBuilder.DropTable(
                name: "Galerii");

            migrationBuilder.DropTable(
                name: "Hoteluri_Oferte");

            migrationBuilder.DropTable(
                name: "Hoteluri_Servicii");

            migrationBuilder.DropTable(
                name: "Recenzii");

            migrationBuilder.DropTable(
                name: "Rezervari");

            migrationBuilder.DropTable(
                name: "Servicii");

            migrationBuilder.DropTable(
                name: "Hoteluri");

            migrationBuilder.DropTable(
                name: "Camere");

            migrationBuilder.DropTable(
                name: "Oferte");

            migrationBuilder.DropTable(
                name: "Utilizatori");

            migrationBuilder.DropTable(
                name: "LanturiHoteliere");
        }
    }
}
