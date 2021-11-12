using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Azure.Migrations
{
    public partial class PolicyEvaluationResultUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompliant",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PolicyEvaluationResultUrl",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompliant",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "PolicyEvaluationResultUrl",
                table: "Subscriptions");
        }
    }
}
