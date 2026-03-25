using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobPortalAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class tv_AddedUserTableAndNavigationPropertiesForCandidateProfileEmployerProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmployerProfiles_user_id",
                table: "EmployerProfiles");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfiles_user_id",
                table: "EmployerProfiles",
                column: "user_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateProfiles_Users_user_id",
                table: "CandidateProfiles",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerProfiles_Users_user_id",
                table: "EmployerProfiles",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CandidateProfiles_Users_user_id",
                table: "CandidateProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerProfiles_Users_user_id",
                table: "EmployerProfiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_EmployerProfiles_user_id",
                table: "EmployerProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfiles_user_id",
                table: "EmployerProfiles",
                column: "user_id");
        }
    }
}
