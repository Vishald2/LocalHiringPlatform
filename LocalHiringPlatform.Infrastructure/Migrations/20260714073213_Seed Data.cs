using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "industryTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "industryTypes",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "industryTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "industryTypes",
                columns: new[] { "IndustryTypeId", "Code", "Description", "DisplayOrder", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "IT", "", 1, true, "Information Technology" },
                    { 2, "MFG", "", 2, true, "Manufacturing" },
                    { 3, "AUTO", "", 3, true, "Automobile" },
                    { 4, "RETAIL", "", 4, true, "Retail" },
                    { 5, "SALES", "", 5, true, "Sales" },
                    { 6, "BPO", "", 6, true, "BPO / Call Center" },
                    { 7, "BANK", "", 7, true, "Banking & Financial Services" },
                    { 8, "HEALTH", "", 8, true, "Healthcare" },
                    { 9, "EDU", "", 9, true, "Education" },
                    { 10, "LOG", "", 10, true, "Logistics & Supply Chain" },
                    { 11, "CONST", "", 11, true, "Construction" },
                    { 12, "HOTEL", "", 12, true, "Hospitality" },
                    { 13, "MEDIA", "", 13, true, "Media & Entertainment" },
                    { 14, "TELECOM", "", 14, true, "Telecommunications" },
                    { 15, "GOVT", "", 15, true, "Government" },
                    { 16, "OTHER", "", 99, true, "Other" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "industryTypes",
                keyColumn: "IndustryTypeId",
                keyValue: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "industryTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "industryTypes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "industryTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
