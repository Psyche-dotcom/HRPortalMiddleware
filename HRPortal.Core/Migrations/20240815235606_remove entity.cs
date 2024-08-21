using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRPortal.Core.Migrations
{
    /// <inheritdoc />
    public partial class removeentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "HomeAddress",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "ContactInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Staffs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Staffs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Staffs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress",
                table: "ContactInfos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomePhone",
                table: "ContactInfos",
                type: "text",
                nullable: true);
        }
    }
}
