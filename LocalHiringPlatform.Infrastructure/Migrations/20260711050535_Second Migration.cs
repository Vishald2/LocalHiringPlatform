using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateCourseSpecializations_CourseSpecializations_CourseSpecializationId",
                table: "CandidateCourseSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_CandidateCourseSpecializations_CourseSpecializationId",
                table: "CandidateCourseSpecializations");

            migrationBuilder.DropColumn(
                name: "CourseSpecializationId",
                table: "CandidateCourseSpecializations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseSpecializationId",
                table: "CandidateCourseSpecializations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCourseSpecializations_CourseSpecializationId",
                table: "CandidateCourseSpecializations",
                column: "CourseSpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateCourseSpecializations_CourseSpecializations_CourseSpecializationId",
                table: "CandidateCourseSpecializations",
                column: "CourseSpecializationId",
                principalTable: "CourseSpecializations",
                principalColumn: "CourseSpecializationId");
        }
    }
}
