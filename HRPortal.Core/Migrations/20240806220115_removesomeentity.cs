using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPortal.Core.Migrations
{
    /// <inheritdoc />
    public partial class removesomeentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_StaffId",
                table: "WorkSchedules");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "DisabilityStatus",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "EmployeePhoto",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "EmploymentStatus",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Ethnicity",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Manager",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "SocialSecurityNumber",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "VeteranStatus",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "WorkLocation",
                table: "Staffs");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_StaffId",
                table: "WorkSchedules",
                column: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkSchedules_StaffId",
                table: "WorkSchedules");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisabilityStatus",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeePhoto",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmploymentStatus",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmploymentType",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Staffs",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ethnicity",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialSecurityNumber",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Staffs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VeteranStatus",
                table: "Staffs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkLocation",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedules_StaffId",
                table: "WorkSchedules",
                column: "StaffId",
                unique: true);
        }
    }
}
