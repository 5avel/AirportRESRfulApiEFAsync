using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirportRESRfulApi.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightNumber = table.Column<string>(maxLength: 8, nullable: false),
                    DeparturePoint = table.Column<string>(maxLength: 50, nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    DestinationPoint = table.Column<string>(maxLength: 50, nullable: false),
                    ArrivalTime = table.Column<DateTime>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaneTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Model = table.Column<string>(maxLength: 50, nullable: false),
                    Seats = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Range = table.Column<int>(nullable: false),
                    ServiceLifeInTicks = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightNumber = table.Column<string>(maxLength: 8, nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    FlightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departures_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<int>(nullable: false),
                    FlightNumber = table.Column<string>(maxLength: 8, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    PlaseNumber = table.Column<int>(nullable: false),
                    IsSold = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DepartureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crews_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    DepartureId = table.Column<int>(nullable: false),
                    PlaneTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planes_Departures_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Departures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Planes_PlaneTypes_PlaneTypeId",
                        column: x => x.PlaneTypeId,
                        principalTable: "PlaneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    CrewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pilots_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stewardesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CrewId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stewardesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stewardesses_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalTime", "DeparturePoint", "DepartureTime", "DestinationPoint", "FlightNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 7, 13, 13, 22, 56, 640, DateTimeKind.Local), "London", new DateTime(2018, 7, 13, 8, 22, 56, 640, DateTimeKind.Local), "Ukraine", "QW11" },
                    { 2, new DateTime(2018, 7, 13, 13, 22, 56, 640, DateTimeKind.Local), "Ukraine", new DateTime(2018, 7, 13, 8, 22, 56, 640, DateTimeKind.Local), "London", "QW12" },
                    { 3, new DateTime(2018, 7, 13, 13, 22, 56, 640, DateTimeKind.Local), "London", new DateTime(2018, 7, 13, 8, 22, 56, 640, DateTimeKind.Local), "Dnipro", "QW13" }
                });

            migrationBuilder.InsertData(
                table: "PlaneTypes",
                columns: new[] { "Id", "Capacity", "Model", "Range", "Seats", "ServiceLifeInTicks" },
                values: new object[,]
                {
                    { 1, 5000, "AN140", 2345, 23, 172800000000000L },
                    { 2, 5000, "IL235", 2345, 23, 216000000000000L },
                    { 3, 5000, "A380", 2345, 23, 259200000000000L }
                });

            migrationBuilder.InsertData(
                table: "Departures",
                columns: new[] { "Id", "DepartureTime", "FlightId", "FlightNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 7, 13, 8, 22, 56, 640, DateTimeKind.Local), 1, "QW11" },
                    { 3, new DateTime(2018, 7, 13, 8, 22, 56, 640, DateTimeKind.Local), 3, "ER86" },
                    { 2, new DateTime(2018, 7, 13, 8, 22, 56, 640, DateTimeKind.Local), 2, "KJ76" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "FlightId", "FlightNumber", "IsSold", "PlaseNumber", "Price" },
                values: new object[,]
                {
                    { 18, 3, "ER86", false, 4, 200m },
                    { 17, 3, "ER86", false, 3, 200m },
                    { 16, 3, "ER86", false, 2, 200m },
                    { 15, 3, "ER86", false, 1, 200m },
                    { 14, 2, "KJ76", false, 6, 200m },
                    { 13, 2, "KJ76", false, 5, 200m },
                    { 12, 2, "KJ76", false, 4, 200m },
                    { 11, 2, "KJ76", false, 3, 200m },
                    { 10, 2, "KJ76", false, 2, 200m },
                    { 9, 2, "KJ76", false, 1, 200m },
                    { 8, 1, "QW11", false, 8, 200m },
                    { 7, 1, "QW11", false, 7, 200m },
                    { 6, 1, "QW11", false, 6, 200m },
                    { 5, 1, "QW11", false, 5, 200m },
                    { 4, 1, "QW11", false, 4, 200m },
                    { 3, 1, "QW11", false, 3, 200m },
                    { 2, 1, "QW11", false, 2, 200m },
                    { 1, 1, "QW11", false, 1, 200m },
                    { 19, 3, "ER86", false, 5, 200m },
                    { 20, 3, "ER86", false, 6, 200m }
                });

            migrationBuilder.InsertData(
                table: "Crews",
                columns: new[] { "Id", "DepartureId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "DepartureId", "Name", "PlaneTypeId", "ReleaseDate" },
                values: new object[,]
                {
                    { 1, 1, "dfg4456", 1, new DateTime(1995, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "QQWS1298", 2, new DateTime(1995, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, "INB677", 3, new DateTime(1995, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Pilots",
                columns: new[] { "Id", "Birthday", "CrewId", "Experience", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, "Ivan", "Ivanov" },
                    { 2, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, "Peta", "Penhjd" },
                    { 3, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 6, "Max", "Maximov" }
                });

            migrationBuilder.InsertData(
                table: "Stewardesses",
                columns: new[] { "Id", "Birthday", "CrewId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ivana", "Ivanova" },
                    { 4, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Ira", "Ivanova" },
                    { 2, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Petra", "Penhjd" },
                    { 5, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Lena", "Penhjd" },
                    { 3, new DateTime(1987, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Maxima", "Maximova" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_DepartureId",
                table: "Crews",
                column: "DepartureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departures_FlightId",
                table: "Departures",
                column: "FlightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_CrewId",
                table: "Pilots",
                column: "CrewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planes_DepartureId",
                table: "Planes",
                column: "DepartureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planes_PlaneTypeId",
                table: "Planes",
                column: "PlaneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Stewardesses_CrewId",
                table: "Stewardesses",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Stewardesses");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "PlaneTypes");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
