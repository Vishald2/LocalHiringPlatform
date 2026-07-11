using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuplicateProfileIdfromCanEduTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateEducations_CandidateProfiles_CandidateProfileEntityId",
                table: "CandidateEducations");

            migrationBuilder.DropIndex(
                name: "IX_CandidateEducations_CandidateProfileEntityId",
                table: "CandidateEducations");

            migrationBuilder.DropColumn(
                name: "CandidateProfileEntityId",
                table: "CandidateEducations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CandidateProfileEntityId",
                table: "CandidateEducations",
                type: "uniqueidentifier",
                nullable: true);

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
        }
    }
}
