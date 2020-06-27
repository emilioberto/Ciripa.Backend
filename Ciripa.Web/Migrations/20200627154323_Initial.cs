using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ciripa.Web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contract",
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
                    MinContractValue = table.Column<decimal>(nullable: false),
                    ContractType = table.Column<int>(nullable: false),
                    WeeklyContract = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
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
                        name: "FK_Kids_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtraPresences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    KidId = table.Column<int>(nullable: false),
                    MorningEntry = table.Column<DateTime>(nullable: true),
                    MorningExit = table.Column<DateTime>(nullable: true),
                    EveningEntry = table.Column<DateTime>(nullable: true),
                    EveningExit = table.Column<DateTime>(nullable: true),
                    SpecialContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraPresences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraPresences_Kids_KidId",
                        column: x => x.KidId,
                        principalTable: "Kids",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtraPresences_Contract_SpecialContractId",
                        column: x => x.SpecialContractId,
                        principalTable: "Contract",
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
                    BillingParentId = table.Column<int>(nullable: false),
                    BillingParent_Id = table.Column<int>(nullable: true),
                    BillingParent_FirstName = table.Column<string>(nullable: true),
                    BillingParent_LastName = table.Column<string>(nullable: true),
                    BillingParent_FiscalCode = table.Column<string>(nullable: true),
                    BillingParent_Phone = table.Column<string>(nullable: true),
                    BillingParent_Email = table.Column<string>(nullable: true),
                    BillingParent_Address = table.Column<string>(nullable: true),
                    BillingParent_City = table.Column<string>(nullable: true),
                    BillingParent_Cap = table.Column<string>(nullable: true),
                    BillingParent_Province = table.Column<string>(nullable: true),
                    BillingParent_Billing = table.Column<bool>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: true),
                    SubscriptionAmount = table.Column<decimal>(nullable: true),
                    SubscriptionPaidDate = table.Column<DateTime>(nullable: true),
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
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 8, 1, 7.5m, "Contratto 7,5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 675m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime", "WeeklyContract" },
                values: new object[] { 15, 2, 0m, "Babysitting", new DateTime(2020, 1, 1, 22, 59, 59, 0, DateTimeKind.Unspecified), 10.0m, 10.0m, 0m, false, 0m, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 14, 1, 0m, "Contratto orario", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8m, 7m, 300m, true, 43m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 13, 1, 10m, "Contratto 10 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 800m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 12, 1, 9.5m, "Contratto 9,5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 775m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 11, 1, 9m, "Contratto 9 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 750m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 10, 1, 8.5m, "Contratto 8,5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 725m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 9, 1, 8m, "Contratto 8 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 700m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime", "WeeklyContract" },
                values: new object[] { 17, 2, 0m, "Aiuto compiti", new DateTime(2020, 1, 1, 22, 59, 59, 0, DateTimeKind.Unspecified), 8.0m, 8.0m, 0m, false, 0m, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 7, 1, 7m, "Contratto 7 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 650m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 6, 1, 6.5m, "Contratto 6,5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 625m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 5, 1, 6m, "Contratto 6 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 600m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 4, 1, 5.5m, "Contratto 5,5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 575m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 3, 1, 5m, "Contratto 5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 550m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 2, 1, 4.5m, "Contratto 4,5 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 525m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime" },
                values: new object[] { 1, 1, 4m, "Contratto 4 ore/dì", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 500m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Contract",
                columns: new[] { "Id", "ContractType", "DailyHours", "Description", "EndTime", "ExtraHourCost", "HourCost", "MinContractValue", "MonthlyContract", "MonthlyHours", "StartTime", "WeeklyContract" },
                values: new object[] { 16, 2, 0m, "Colonie settimanali", new DateTime(2020, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 8.0m, 7.0m, 150m, false, 0m, new DateTime(2020, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), true });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "ExtraHourCost", "HourCost", "SubscriptionAmount" },
                values: new object[] { 1, 7.0m, 6.0m, 200.0m });

            migrationBuilder.CreateIndex(
                name: "IX_ExtraPresences_KidId",
                table: "ExtraPresences",
                column: "KidId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraPresences_SpecialContractId",
                table: "ExtraPresences",
                column: "SpecialContractId");

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
                name: "ExtraPresences");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Presences");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Kids");

            migrationBuilder.DropTable(
                name: "Contract");
        }
    }
}
