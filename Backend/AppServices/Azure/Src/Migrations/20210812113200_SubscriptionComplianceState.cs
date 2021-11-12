using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Azure.Migrations
{
    public partial class SubscriptionComplianceState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompliant",
                table: "Subscriptions");

            migrationBuilder.AddColumn<int>(
                name: "ComplianceState",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplianceState",
                table: "Subscriptions");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompliant",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
