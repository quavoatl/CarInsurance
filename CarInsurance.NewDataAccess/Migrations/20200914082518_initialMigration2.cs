using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInsurance.NewDataAccess.Migrations
{
    public partial class initialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LimitType",
                table: "Limit");

            migrationBuilder.DropIndex(
                name: "IX_Limit_CoverLimitCoverId",
                table: "Limit");

            migrationBuilder.DropColumn(
                name: "CoverLimitCoverId",
                table: "Limit");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BrokerPolicyTemplate",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Address = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    IsBroker = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Limit_LimitCoverId",
                table: "Limit",
                column: "LimitCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerPolicyTemplate_AppUserId",
                table: "BrokerPolicyTemplate",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrokerPolicyTemplate_AppUser_AppUserId",
                table: "BrokerPolicyTemplate",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitType",
                table: "Limit",
                column: "LimitCoverId",
                principalTable: "Cover",
                principalColumn: "LimitCoverId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrokerPolicyTemplate_AppUser_AppUserId",
                table: "BrokerPolicyTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitType",
                table: "Limit");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropIndex(
                name: "IX_Limit_LimitCoverId",
                table: "Limit");

            migrationBuilder.DropIndex(
                name: "IX_BrokerPolicyTemplate_AppUserId",
                table: "BrokerPolicyTemplate");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BrokerPolicyTemplate");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverLimitCoverId",
                table: "Limit",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Limit_CoverLimitCoverId",
                table: "Limit",
                column: "CoverLimitCoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_LimitType",
                table: "Limit",
                column: "CoverLimitCoverId",
                principalTable: "Cover",
                principalColumn: "LimitCoverId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
