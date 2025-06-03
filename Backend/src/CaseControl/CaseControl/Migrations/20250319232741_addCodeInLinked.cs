using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class addCodeInLinked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Linkeds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Linkeds");
        }
    }
}
