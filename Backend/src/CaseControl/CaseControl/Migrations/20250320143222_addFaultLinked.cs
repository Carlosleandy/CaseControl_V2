using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class addFaultLinked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaultLinkeds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseID = table.Column<int>(type: "int", nullable: false),
                    LinkedID = table.Column<int>(type: "int", nullable: false),
                    FaultID = table.Column<int>(type: "int", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultLinkeds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FaultLinkeds_Cases_CaseID",
                        column: x => x.CaseID,
                        principalTable: "Cases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaultLinkeds_Faults_FaultID",
                        column: x => x.FaultID,
                        principalTable: "Faults",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaultLinkeds_Linkeds_LinkedID",
                        column: x => x.LinkedID,
                        principalTable: "Linkeds",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaultLinkeds_CaseID",
                table: "FaultLinkeds",
                column: "CaseID");

            migrationBuilder.CreateIndex(
                name: "IX_FaultLinkeds_FaultID",
                table: "FaultLinkeds",
                column: "FaultID");

            migrationBuilder.CreateIndex(
                name: "IX_FaultLinkeds_LinkedID",
                table: "FaultLinkeds",
                column: "LinkedID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaultLinkeds");
        }
    }
}
