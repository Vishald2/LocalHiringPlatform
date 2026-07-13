using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateCourseSpecializations");

            migrationBuilder.CreateTable(
                name: "CandidateEducationSpecializations",
                columns: table => new
                {
                    CandidateEducationSpecializationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateEducationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducationSpecializations", x => x.CandidateEducationSpecializationId);
                    table.ForeignKey(
                        name: "FK_CandidateEducationSpecializations_CandidateEducations_CandidateEducationEntityId",
                        column: x => x.CandidateEducationEntityId,
                        principalTable: "CandidateEducations",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateEducationSpecializations_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecializations_CandidateEducationEntityId_SpecializationId",
                table: "CandidateEducationSpecializations",
                columns: new[] { "CandidateEducationEntityId", "SpecializationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecializations_SpecializationId",
                table: "CandidateEducationSpecializations",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateEducationSpecializations");

            migrationBuilder.CreateTable(
                name: "CandidateCourseSpecializations",
                columns: table => new
                {
                    CandidateEducationSpecializationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateEducationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCourseSpecializations", x => x.CandidateEducationSpecializationId);
                    table.ForeignKey(
                        name: "FK_CandidateCourseSpecializations_CandidateEducations_CandidateEducationEntityId",
                        column: x => x.CandidateEducationEntityId,
                        principalTable: "CandidateEducations",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateCourseSpecializations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CandidateCourseSpecializations_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCourseSpecializations_CandidateEducationEntityId_SpecializationId",
                table: "CandidateCourseSpecializations",
                columns: new[] { "CandidateEducationEntityId", "SpecializationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCourseSpecializations_CourseId",
                table: "CandidateCourseSpecializations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCourseSpecializations_SpecializationId",
                table: "CandidateCourseSpecializations",
                column: "SpecializationId");
        }
    }
}
