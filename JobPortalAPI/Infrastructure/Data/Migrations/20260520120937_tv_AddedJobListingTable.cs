using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortalAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class tv_AddedJobListingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "JobPostings",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "job_description",
                table: "JobPostings",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "JobPostings",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "salary_from",
                table: "JobPostings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "salary_to",
                table: "JobPostings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "salary_from",
                table: "JobPostings");

            migrationBuilder.DropColumn(
                name: "salary_to",
                table: "JobPostings");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "JobPostings",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<string>(
                name: "job_description",
                table: "JobPostings",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);
        }
    }
}
