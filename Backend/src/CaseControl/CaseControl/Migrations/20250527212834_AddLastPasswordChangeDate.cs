using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLastPasswordChangeDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastPasswordChangeDate",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPasswordChangeDate",
                table: "Users");
        }
    }
}
