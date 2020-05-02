using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ciripa.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kids",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FiscalCode = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: true),
                    ContractType = table.Column<int>(nullable: false),
                    ContractValue = table.Column<decimal>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    SubscriptionPaid = table.Column<bool>(nullable: false),
                    SubscriptionAmount = table.Column<decimal>(nullable: false),
                    ParentFirstName = table.Column<string>(nullable: true),
                    ParentLastName = table.Column<string>(nullable: true),
                    ParentFiscalCode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HourCost = table.Column<decimal>(nullable: false),
                    ExtraHourCost = table.Column<decimal>(nullable: false),
                    SubscriptionAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KidId = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true),
                    Hours = table.Column<decimal>(nullable: true),
                    InvoiceAmount = table.Column<decimal>(nullable: true),
                    PaymentMethod = table.Column<int>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Presences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    KidId = table.Column<int>(nullable: false),
                    MorningEntry = table.Column<DateTime>(nullable: true),
                    MorningExit = table.Column<DateTime>(nullable: true),
                    EveningEntry = table.Column<DateTime>(nullable: true),
                    EveningExit = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Presences_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "ExtraHourCost", "HourCost", "SubscriptionAmount" },
                values: new object[] { 1, 7.0m, 6.0m, 200.0m });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_KidId",
                table: "Invoice",
                column: "KidId");

            migrationBuilder.CreateIndex(
                name: "IX_Presences_KidId",
                table: "Presences",
                column: "KidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Presences");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Kids");
        }
    }
}
