using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPortal.Core.Migrations
{
    /// <inheritdoc />
    public partial class updatestaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_AspNetUsers_ApplicationCompanyId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_ApplicationCompanyId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "ApplicationCompanyId",
                table: "Staffs");

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_CompanyId",
                table: "Staffs",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_AspNetUsers_CompanyId",
                table: "Staffs",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_AspNetUsers_CompanyId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_CompanyId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Staffs");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationCompanyId",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_ApplicationCompanyId",
                table: "Staffs",
                column: "ApplicationCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_AspNetUsers_ApplicationCompanyId",
                table: "Staffs",
                column: "ApplicationCompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
