using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingCityStateCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "CandidateEducations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CandidateEducations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "CandidateEducations",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "CandidateEducations");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "CandidateEducations");

            migrationBuilder.DropColumn(
                name: "State",
                table: "CandidateEducations");
        }
    }
}
