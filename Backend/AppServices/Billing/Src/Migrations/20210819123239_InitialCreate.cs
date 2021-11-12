using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Billing.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CloudAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayerAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    BaseData_CostCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_ProfitCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_CostCenterResponsiblePrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_ControllingContactPrincipalName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayerAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostType = table.Column<int>(type: "int", nullable: false),
                    CostId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloudAccountId = table.Column<int>(type: "int", nullable: false),
                    CostDetails_Amount = table.Column<float>(type: "real", nullable: true),
                    CostDetails_Currency = table.Column<int>(type: "int", nullable: true),
                    CostDetails_PeriodBegin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CostDetails_PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CostDetails_PeriodId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Costs_CloudAccounts_CloudAccountId",
                        column: x => x.CloudAccountId,
                        principalTable: "CloudAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllocationKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CloudAccountId = table.Column<int>(type: "int", nullable: false),
                    PayerAccountId = table.Column<int>(type: "int", nullable: false),
                    BaseData_AllocationPercentage = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllocationKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllocationKeys_CloudAccounts_CloudAccountId",
                        column: x => x.CloudAccountId,
                        principalTable: "CloudAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllocationKeys_PayerAccounts_PayerAccountId",
                        column: x => x.PayerAccountId,
                        principalTable: "PayerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllocationKeys_CloudAccountId",
                table: "AllocationKeys",
                column: "CloudAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AllocationKeys_PayerAccountId",
                table: "AllocationKeys",
                column: "PayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CloudAccountId",
                table: "Costs",
                column: "CloudAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllocationKeys");

            migrationBuilder.DropTable(
                name: "Costs");

            migrationBuilder.DropTable(
                name: "PayerAccounts");

            migrationBuilder.DropTable(
                name: "CloudAccounts");
        }
    }
}
