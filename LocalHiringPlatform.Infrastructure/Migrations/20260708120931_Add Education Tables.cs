using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEducationTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.SpecializationId);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "EducationId");
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducations",
                columns: table => new
                {
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: true),
                    InstituteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartYear = table.Column<int>(type: "int", nullable: true),
                    EndYear = table.Column<int>(type: "int", nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    CGPA = table.Column<decimal>(type: "decimal(4,2)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsHighestEducation = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducations", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileId",
                        column: x => x.CandidateProfileId,
                        principalTable: "CandidateProfiles",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateEducations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CandidateEducations_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "EducationId");
                    table.ForeignKey(
                        name: "FK_CandidateEducations_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "UniversityId");
                });

            migrationBuilder.CreateTable(
                name: "CourseSpecializations",
                columns: table => new
                {
                    CourseSpecializationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSpecializations", x => x.CourseSpecializationId);
                    table.ForeignKey(
                        name: "FK_CourseSpecializations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CourseSpecializations_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "SpecializationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateEducationSpecialization",
                columns: table => new
                {
                    CandidateEducationSpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateEducationEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseSpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateEducationSpecialization", x => x.CandidateEducationSpecializationId);
                    table.ForeignKey(
                        name: "FK_CandidateEducationSpecialization_CandidateEducations_CandidateEducationEntityId",
                        column: x => x.CandidateEducationEntityId,
                        principalTable: "CandidateEducations",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateEducationSpecialization_CourseSpecializations_CourseSpecializationId",
                        column: x => x.CourseSpecializationId,
                        principalTable: "CourseSpecializations",
                        principalColumn: "CourseSpecializationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_CandidateProfileId",
                table: "CandidateEducations",
                column: "CandidateProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_CourseId",
                table: "CandidateEducations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_EducationId",
                table: "CandidateEducations",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_UniversityId",
                table: "CandidateEducations",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_CandidateEducationEntityId_CourseSpecializationId",
                table: "CandidateEducationSpecialization",
                columns: new[] { "CandidateEducationEntityId", "CourseSpecializationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_CourseSpecializationId",
                table: "CandidateEducationSpecialization",
                column: "CourseSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Code",
                table: "Courses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EducationId",
                table: "Courses",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSpecializations_CourseId",
                table: "CourseSpecializations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSpecializations_SpecializationId",
                table: "CourseSpecializations",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateEducationSpecialization");

            migrationBuilder.DropTable(
                name: "CandidateEducations");

            migrationBuilder.DropTable(
                name: "CourseSpecializations");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Specialization");

            migrationBuilder.DropTable(
                name: "Educations");
        }
    }
}
