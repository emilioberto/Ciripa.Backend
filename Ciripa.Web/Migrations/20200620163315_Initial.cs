using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ciripa.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(nullable: true),
                    MonthlyContract = table.Column<bool>(nullable: false),
                    DailyHours = table.Column<decimal>(nullable: false),
                    MonthlyHours = table.Column<decimal>(nullable: false),
                    HourCost = table.Column<decimal>(nullable: false),
                    ExtraHourCost = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: true),
                    MinContractValue = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
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
                    ContractId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    SubscriptionPaidDate = table.Column<DateTime>(nullable: true),
                    SubscriptionAmount = table.Column<decimal>(nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    ExtraServicesEnabled = table.Column<bool>(nullable: false),
                    Parent1_Id = table.Column<int>(nullable: true),
                    Parent1_FirstName = table.Column<string>(nullable: true),
                    Parent1_LastName = table.Column<string>(nullable: true),
                    Parent1_FiscalCode = table.Column<string>(nullable: true),
                    Parent1_Phone = table.Column<string>(nullable: true),
                    Parent1_Email = table.Column<string>(nullable: true),
                    Parent1_Address = table.Column<string>(nullable: true),
                    Parent1_City = table.Column<string>(nullable: true),
                    Parent1_Cap = table.Column<string>(nullable: true),
                    Parent1_Province = table.Column<string>(nullable: true),
                    Parent1_Billing = table.Column<bool>(nullable: true),
                    Parent2_Id = table.Column<int>(nullable: true),
                    Parent2_FirstName = table.Column<string>(nullable: true),
                    Parent2_LastName = table.Column<string>(nullable: true),
                    Parent2_FiscalCode = table.Column<string>(nullable: true),
                    Parent2_Phone = table.Column<string>(nullable: true),
                    Parent2_Email = table.Column<string>(nullable: true),
                    Parent2_Address = table.Column<string>(nullable: true),
                    Parent2_City = table.Column<string>(nullable: true),
                    Parent2_Cap = table.Column<string>(nullable: true),
                    Parent2_Province = table.Column<string>(nullable: true),
                    Parent2_Billing = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kids_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Kids_ContractId",
                table: "Kids",
                column: "ContractId");

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

            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
