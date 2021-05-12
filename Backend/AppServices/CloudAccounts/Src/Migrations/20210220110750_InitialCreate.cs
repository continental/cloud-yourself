using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudYourself.Backend.AppServices.CloudAccounts.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseData_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CloudAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    BaseData_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_Division = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_LegalEntity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseData_OperationalContact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloudAccounts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CloudAccounts_TenantId",
                table: "CloudAccounts",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CloudAccounts");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
