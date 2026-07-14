using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableNameCapitalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperiences_industryTypes_IndustryTypeId",
                table: "CandidateExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_industryTypes",
                table: "industryTypes");

            migrationBuilder.RenameTable(
                name: "industryTypes",
                newName: "IndustryTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndustryTypes",
                table: "IndustryTypes",
                column: "IndustryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperiences_IndustryTypes_IndustryTypeId",
                table: "CandidateExperiences",
                column: "IndustryTypeId",
                principalTable: "IndustryTypes",
                principalColumn: "IndustryTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateExperiences_IndustryTypes_IndustryTypeId",
                table: "CandidateExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndustryTypes",
                table: "IndustryTypes");

            migrationBuilder.RenameTable(
                name: "IndustryTypes",
                newName: "industryTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_industryTypes",
                table: "industryTypes",
                column: "IndustryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateExperiences_industryTypes_IndustryTypeId",
                table: "CandidateExperiences",
                column: "IndustryTypeId",
                principalTable: "industryTypes",
                principalColumn: "IndustryTypeId");
        }
    }
}
