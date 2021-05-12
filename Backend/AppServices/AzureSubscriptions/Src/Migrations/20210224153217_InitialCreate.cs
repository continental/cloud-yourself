using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.AzureSubscriptions.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AppRegistration_AzureDirectoryTenantId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppRegistration_AzureAppRegistrationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppRegistration_AzureAppSecret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagementTarget_EnrollmentAccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagementTarget_ManagementGroupId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CloudAccountId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreationOperationUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_TenantId",
                table: "Subscriptions",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
