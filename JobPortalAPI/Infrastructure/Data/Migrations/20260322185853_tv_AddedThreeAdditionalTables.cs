using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobPortalAPI.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class tv_AddedThreeAdditionalTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "contact_email",
                table: "EmployerProfiles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "JobPostings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employer_profile_id = table.Column<int>(type: "integer", nullable: false),
                    job_description = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostings", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobPostings_EmployerProfiles_employer_profile_id",
                        column: x => x.employer_profile_id,
                        principalTable: "EmployerProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    candidate_profile_id = table.Column<int>(type: "integer", nullable: false),
                    job_posting_id = table.Column<int>(type: "integer", nullable: false),
                    applied_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_Applications_CandidateProfiles_candidate_profile_id",
                        column: x => x.candidate_profile_id,
                        principalTable: "CandidateProfiles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_JobPostings_job_posting_id",
                        column: x => x.job_posting_id,
                        principalTable: "JobPostings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    job_posting_id = table.Column<int>(type: "integer", nullable: false),
                    skill_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkills", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobSkills_JobPostings_job_posting_id",
                        column: x => x.job_posting_id,
                        principalTable: "JobPostings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobSkills_Skills_skill_id",
                        column: x => x.skill_id,
                        principalTable: "Skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerProfiles_user_id",
                table: "EmployerProfiles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_candidate_profile_id_skill_id",
                table: "CandidateSkills",
                columns: new[] { "candidate_profile_id", "skill_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_candidate_profile_id",
                table: "Applications",
                column: "candidate_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_candidate_profile_id_job_posting_id",
                table: "Applications",
                columns: new[] { "candidate_profile_id", "job_posting_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_job_posting_id",
                table: "Applications",
                column: "job_posting_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostings_employer_profile_id",
                table: "JobPostings",
                column: "employer_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_job_posting_id",
                table: "JobSkills",
                column: "job_posting_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_job_posting_id_skill_id",
                table: "JobSkills",
                columns: new[] { "job_posting_id", "skill_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_skill_id",
                table: "JobSkills",
                column: "skill_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "JobSkills");

            migrationBuilder.DropTable(
                name: "JobPostings");

            migrationBuilder.DropIndex(
                name: "IX_EmployerProfiles_user_id",
                table: "EmployerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_CandidateSkills_candidate_profile_id_skill_id",
                table: "CandidateSkills");

            migrationBuilder.DropColumn(
                name: "contact_email",
                table: "EmployerProfiles");
        }
    }
}
