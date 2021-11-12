using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Azure.Migrations
{
    public partial class ComplianceValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplianceState",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "PolicyEvaluationResultUrl",
                table: "Subscriptions",
                newName: "Compliance_PolicyEvaluationResultUrl");

            migrationBuilder.AddColumn<int>(
                name: "Compliance_State",
                table: "Subscriptions",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compliance_State",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "Compliance_PolicyEvaluationResultUrl",
                table: "Subscriptions",
                newName: "PolicyEvaluationResultUrl");

            migrationBuilder.AddColumn<int>(
                name: "ComplianceState",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
