using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Azure.Migrations
{
    public partial class TwoPhaseCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComplianceSettings_InitiativeAssignmentName",
                table: "ManagedResources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ComplianceSettings_InitiativeDefinitionId",
                table: "ManagedResources",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CommitDate",
                table: "ManagedResourceDeployments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ManagedResourceDeployments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PrepareDate",
                table: "ManagedResourceDeployments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "ManagedResourceDeployments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComplianceSettings_InitiativeAssignmentName",
                table: "ManagedResources");

            migrationBuilder.DropColumn(
                name: "ComplianceSettings_InitiativeDefinitionId",
                table: "ManagedResources");

            migrationBuilder.DropColumn(
                name: "CommitDate",
                table: "ManagedResourceDeployments");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ManagedResourceDeployments");

            migrationBuilder.DropColumn(
                name: "PrepareDate",
                table: "ManagedResourceDeployments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ManagedResourceDeployments");
        }
    }
}
