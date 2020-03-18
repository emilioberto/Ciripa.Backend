using Microsoft.EntityFrameworkCore.Migrations;

namespace Ciripa.Web.Migrations
{
    public partial class Settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HourCost = table.Column<decimal>(nullable: false),
                    ExtraHourCost = table.Column<decimal>(nullable: false),
                    ContractAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "ContractAmount", "ExtraHourCost", "HourCost" },
                values: new object[] { 1, 200.0m, 7.0m, 6.0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
