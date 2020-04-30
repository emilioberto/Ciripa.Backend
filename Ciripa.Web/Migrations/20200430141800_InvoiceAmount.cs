using Microsoft.EntityFrameworkCore.Migrations;

namespace Ciripa.Web.Migrations
{
    public partial class InvoiceAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceAmount",
                table: "Invoice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "Invoice");
        }
    }
}
