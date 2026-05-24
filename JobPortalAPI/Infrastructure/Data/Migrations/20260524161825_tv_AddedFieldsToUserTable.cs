using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortalAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class tv_AddedFieldsToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_login",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "Users",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "refresh_token_expiration_date",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_login",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "refresh_token_expiration_date",
                table: "Users");
        }
    }
}
