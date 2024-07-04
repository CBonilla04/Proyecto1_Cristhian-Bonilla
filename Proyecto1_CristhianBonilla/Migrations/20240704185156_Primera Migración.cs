using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto1_CristhianBonilla.Migrations
{
    /// <inheritdoc />
    public partial class PrimeraMigración : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    IdFlight = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "Smalldatetime", nullable: false),
                    ArriveDate = table.Column<DateTime>(type: "Smalldatetime", nullable: false),
                    totalPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.IdFlight);
                });

            migrationBuilder.CreateTable(
                name: "PassengerType",
                columns: table => new
                {
                    IdPasTyp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerType", x => x.IdPasTyp);
                });

            migrationBuilder.CreateTable(
                name: "Scales",
                columns: table => new
                {
                    IdScale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArriveDate = table.Column<DateTime>(type: "Smalldatetime", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "Smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scales", x => x.IdScale);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Preferences = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "FlightScales",
                columns: table => new
                {
                    IdFliSca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdScale = table.Column<int>(type: "int", nullable: false),
                    IdFlight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightScales", x => x.IdFliSca);
                    table.ForeignKey(
                        name: "FK_FlightScales_Flights_IdFlight",
                        column: x => x.IdFlight,
                        principalTable: "Flights",
                        principalColumn: "IdFlight",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightScales_Scales_IdScale",
                        column: x => x.IdScale,
                        principalTable: "Scales",
                        principalColumn: "IdScale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PayState = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "Smalldatetime", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdFlight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_IdFlight",
                        column: x => x.IdFlight,
                        principalTable: "Flights",
                        principalColumn: "IdFlight",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightPassengers",
                columns: table => new
                {
                    IdFliPas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitPrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IdReservation = table.Column<int>(type: "int", nullable: false),
                    IdPasTyp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPassengers", x => x.IdFliPas);
                    table.ForeignKey(
                        name: "FK_FlightPassengers_PassengerType_IdPasTyp",
                        column: x => x.IdPasTyp,
                        principalTable: "PassengerType",
                        principalColumn: "IdPasTyp",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightPassengers_Reservations_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "Reservations",
                        principalColumn: "IdReservation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_IdPasTyp",
                table: "FlightPassengers",
                column: "IdPasTyp");

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassengers_IdReservation",
                table: "FlightPassengers",
                column: "IdReservation");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScales_IdFlight",
                table: "FlightScales",
                column: "IdFlight");

            migrationBuilder.CreateIndex(
                name: "IX_FlightScales_IdScale",
                table: "FlightScales",
                column: "IdScale");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdFlight",
                table: "Reservations",
                column: "IdFlight");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdUser",
                table: "Reservations",
                column: "IdUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightPassengers");

            migrationBuilder.DropTable(
                name: "FlightScales");

            migrationBuilder.DropTable(
                name: "PassengerType");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Scales");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
