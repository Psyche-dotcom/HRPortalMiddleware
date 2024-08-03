using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPortal.Core.Migrations
{
    /// <inheritdoc />
    public partial class LabourEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LabourId",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Labours",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Customer = table.Column<string>(type: "text", nullable: false),
                    LCAT = table.Column<string>(type: "text", nullable: false),
                    WorkSite = table.Column<string>(type: "text", nullable: false),
                    ChargeCode = table.Column<string>(type: "text", nullable: false),
                    ReminderDays = table.Column<string>(type: "text", nullable: false),
                    Reminder = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labours", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_LabourId",
                table: "Staffs",
                column: "LabourId");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Labours_LabourId",
                table: "Staffs",
                column: "LabourId",
                principalTable: "Labours",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Labours_LabourId",
                table: "Staffs");

            migrationBuilder.DropTable(
                name: "Labours");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_LabourId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "LabourId",
                table: "Staffs");
        }
    }
}
