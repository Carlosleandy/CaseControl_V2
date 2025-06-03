using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseControl.Api.Migrations
{
    /// <inheritdoc />
    public partial class caseassignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingGroups_Name",
                table: "WorkingGroups");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationTypes_Name",
                table: "RecommendationTypes");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationStatuses_Name",
                table: "RecommendationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_Title",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_ReceptionMedia_Name",
                table: "ReceptionMedia");

            migrationBuilder.DropIndex(
                name: "IX_LinkTypes_Name",
                table: "LinkTypes");

            migrationBuilder.DropIndex(
                name: "IX_EvidenceClassifications_Name",
                table: "EvidenceClassifications");

            migrationBuilder.DropIndex(
                name: "IX_CaseTypes_Name",
                table: "CaseTypes");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingGroups_Name",
                table: "WorkingGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationTypes_Name",
                table: "RecommendationTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationStatuses_Name",
                table: "RecommendationStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_Title",
                table: "Recommendations",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionMedia_Name",
                table: "ReceptionMedia",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LinkTypes_Name",
                table: "LinkTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceClassifications_Name",
                table: "EvidenceClassifications",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseTypes_Name",
                table: "CaseTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingGroups_Name",
                table: "WorkingGroups");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationTypes_Name",
                table: "RecommendationTypes");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationStatuses_Name",
                table: "RecommendationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_Title",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_ReceptionMedia_Name",
                table: "ReceptionMedia");

            migrationBuilder.DropIndex(
                name: "IX_LinkTypes_Name",
                table: "LinkTypes");

            migrationBuilder.DropIndex(
                name: "IX_EvidenceClassifications_Name",
                table: "EvidenceClassifications");

            migrationBuilder.DropIndex(
                name: "IX_CaseTypes_Name",
                table: "CaseTypes");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingGroups_Name",
                table: "WorkingGroups",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationTypes_Name",
                table: "RecommendationTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationStatuses_Name",
                table: "RecommendationStatuses",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_Title",
                table: "Recommendations",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionMedia_Name",
                table: "ReceptionMedia",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_LinkTypes_Name",
                table: "LinkTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EvidenceClassifications_Name",
                table: "EvidenceClassifications",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTypes_Name",
                table: "CaseTypes",
                column: "Name");
        }
    }
}
