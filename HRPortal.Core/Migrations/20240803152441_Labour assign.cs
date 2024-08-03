using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPortal.Core.Migrations
{
    /// <inheritdoc />
    public partial class Labourassign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "Labours",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Labours_CompanyId",
                table: "Labours",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Labours_AspNetUsers_CompanyId",
                table: "Labours",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Labours_AspNetUsers_CompanyId",
                table: "Labours");

            migrationBuilder.DropIndex(
                name: "IX_Labours_CompanyId",
                table: "Labours");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Labours");
        }
    }
}
