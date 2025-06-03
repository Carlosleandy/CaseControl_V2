using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class addDateRegistrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Interviews",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInterview",
                table: "Interviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistered",
                table: "Interviews",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEvidence",
                table: "Evidences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRegistered",
                table: "Evidences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateInterview",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "DateEvidence",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "DateRegistered",
                table: "Evidences");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Interviews",
                newName: "Name");
        }
    }
}
