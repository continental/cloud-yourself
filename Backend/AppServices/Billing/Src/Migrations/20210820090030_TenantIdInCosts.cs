using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Billing.Migrations
{
    public partial class TenantIdInCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Costs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Costs");
        }
    }
}
