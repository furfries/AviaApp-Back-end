using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AddFlights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirportFromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirportToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Airplane = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_AirportFromId",
                        column: x => x.AirportFromId,
                        principalTable: "Airports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Flights_Airports_AirportToId",
                        column: x => x.AirportToId,
                        principalTable: "Airports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirportFromId",
                table: "Flights",
                column: "AirportFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirportToId",
                table: "Flights",
                column: "AirportToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
