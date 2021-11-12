using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.Azure.Migrations
{
    public partial class RefactorManagedResorces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagedResources_TemplateSpecsResourceGroup",
                table: "TenantSettings");

            migrationBuilder.CreateTable(
                name: "ManagedResources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    BaseData_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArmTemplate_Template = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedResources_TenantSettings_TenantId",
                        column: x => x.TenantId,
                        principalTable: "TenantSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagedResourceDeployments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagedResourceId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagedResourceDeployments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManagedResourceDeployments_ManagedResources_ManagedResourceId",
                        column: x => x.ManagedResourceId,
                        principalTable: "ManagedResources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ManagedResourceDeployments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManagedResourceDeployments_ManagedResourceId",
                table: "ManagedResourceDeployments",
                column: "ManagedResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedResourceDeployments_SubscriptionId",
                table: "ManagedResourceDeployments",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedResources_TenantId",
                table: "ManagedResources",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagedResourceDeployments");

            migrationBuilder.DropTable(
                name: "ManagedResources");

            migrationBuilder.AddColumn<string>(
                name: "ManagedResources_TemplateSpecsResourceGroup",
                table: "TenantSettings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
