using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Redesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducations_Educations_EducationId",
                table: "CandidateEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducationSpecialization_CandidateEducations_CandidateEducationEntityId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSpecializations_Specialization_SpecializationId",
                table: "CourseSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_CourseSpecializations_CourseId",
                table: "CourseSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducationSpecialization_CandidateEducationEntityId_CourseSpecializationId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducations_EducationId",
                table: "CandidateEducations");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "CandidateEducations");

            migrationBuilder.AlterColumn<int>(
                name: "CourseSpecializationId",
                table: "CandidateEducationSpecialization",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "CandidateEducationEntityId",
                table: "CandidateEducationSpecialization",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "CandidateEducationSpecialization",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfileId",
                table: "CandidateEducationSpecialization",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "CandidateEducationSpecialization",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CandidateProfileEntityId",
                table: "CandidateEducations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Code",
                table: "Universities",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specialization_Code",
                table: "Specialization",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educations_Code",
                table: "Educations",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSpecializations_CourseId_SpecializationId",
                table: "CourseSpecializations",
                columns: new[] { "CourseId", "SpecializationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_CandidateEducationEntityId",
                table: "CandidateEducationSpecialization",
                column: "CandidateEducationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_CourseId_ProfileId_SpecializationId",
                table: "CandidateEducationSpecialization",
                columns: new[] { "CourseId", "ProfileId", "SpecializationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_ProfileId",
                table: "CandidateEducationSpecialization",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_SpecializationId",
                table: "CandidateEducationSpecialization",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_CandidateProfileEntityId",
                table: "CandidateEducations",
                column: "CandidateProfileEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileEntityId",
                table: "CandidateEducations",
                column: "CandidateProfileEntityId",
                principalTable: "CandidateProfiles",
                principalColumn: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducationSpecialization_CandidateEducations_CandidateEducationEntityId",
                table: "CandidateEducationSpecialization",
                column: "CandidateEducationEntityId",
                principalTable: "CandidateEducations",
                principalColumn: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducationSpecialization_CandidateProfiles_ProfileId",
                table: "CandidateEducationSpecialization",
                column: "ProfileId",
                principalTable: "CandidateProfiles",
                principalColumn: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducationSpecialization_Courses_CourseId",
                table: "CandidateEducationSpecialization",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducationSpecialization_Specialization_SpecializationId",
                table: "CandidateEducationSpecialization",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSpecializations_Specialization_SpecializationId",
                table: "CourseSpecializations",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileEntityId",
                table: "CandidateEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducationSpecialization_CandidateEducations_CandidateEducationEntityId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducationSpecialization_CandidateProfiles_ProfileId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducationSpecialization_Courses_CourseId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducationSpecialization_Specialization_SpecializationId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseSpecializations_Specialization_SpecializationId",
                table: "CourseSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_Universities_Code",
                table: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_Specialization_Code",
                table: "Specialization");

            migrationBuilder.DropIndex(
                name: "IX_Educations_Code",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_CourseSpecializations_CourseId_SpecializationId",
                table: "CourseSpecializations");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducationSpecialization_CandidateEducationEntityId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducationSpecialization_CourseId_ProfileId_SpecializationId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducationSpecialization_ProfileId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducationSpecialization_SpecializationId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducations_CandidateProfileEntityId",
                table: "CandidateEducations");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "CandidateEducationSpecialization");

            migrationBuilder.DropColumn(
                name: "CandidateProfileEntityId",
                table: "CandidateEducations");

            migrationBuilder.AlterColumn<int>(
                name: "CourseSpecializationId",
                table: "CandidateEducationSpecialization",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CandidateEducationEntityId",
                table: "CandidateEducationSpecialization",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "CandidateEducations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseSpecializations_CourseId",
                table: "CourseSpecializations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducationSpecialization_CandidateEducationEntityId_CourseSpecializationId",
                table: "CandidateEducationSpecialization",
                columns: new[] { "CandidateEducationEntityId", "CourseSpecializationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateEducations_EducationId",
                table: "CandidateEducations",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducations_Educations_EducationId",
                table: "CandidateEducations",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateEducationSpecialization_CandidateEducations_CandidateEducationEntityId",
                table: "CandidateEducationSpecialization",
                column: "CandidateEducationEntityId",
                principalTable: "CandidateEducations",
                principalColumn: "EntityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseSpecializations_Specialization_SpecializationId",
                table: "CourseSpecializations",
                column: "SpecializationId",
                principalTable: "Specialization",
                principalColumn: "SpecializationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
