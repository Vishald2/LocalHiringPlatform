using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalHiringPlatform.Infrastructure.Migrations
{
    public partial class Emailmobileverification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailVerificationToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerifiedOn",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileVerificationCode",
                table: "Users",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MobileVerificationCodeExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MobileVerified",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "MobileVerifiedOn",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerificationToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailVerifiedOn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MobileVerificationCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MobileVerificationCodeExpiry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MobileVerified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MobileVerifiedOn",
                table: "Users");
        }
    }
}
