using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class addgerencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GerenciaID",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gerencias",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerencias", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_GerenciaID",
                table: "Cases",
                column: "GerenciaID");

            migrationBuilder.CreateIndex(
                name: "IX_Gerencias_Name",
                table: "Gerencias",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Gerencias_GerenciaID",
                table: "Cases",
                column: "GerenciaID",
                principalTable: "Gerencias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Gerencias_GerenciaID",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Gerencias");

            migrationBuilder.DropIndex(
                name: "IX_Cases_GerenciaID",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "GerenciaID",
                table: "Cases");
        }
    }
}
