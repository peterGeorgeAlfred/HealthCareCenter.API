using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelCareCenter.Context.Migrations
{
    public partial class UpdateAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Patient",
                newName: "User_Identity");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "Patient",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "Patient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isAccepted",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "isAccepted",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "User_Identity",
                table: "Patient",
                newName: "Name");
        }
    }
}
