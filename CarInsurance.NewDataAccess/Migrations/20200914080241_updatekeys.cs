using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInsurance.NewDataAccess.Migrations
{
    public partial class updatekeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarType",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRuleType",
                table: "CarRule");

            migrationBuilder.DropForeignKey(
                name: "FK_CoverType",
                table: "Cover");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitType",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitRuleType",
                table: "LimitRule");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionType",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LimitRule",
                table: "LimitRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Limit",
                table: "Limit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cover",
                table: "Cover");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRule",
                table: "CarRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverQuestionCoverId",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LimitRuleId",
                table: "LimitRule",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "LimitRuleCoverId1",
                table: "LimitRule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LimitId",
                table: "Limit",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverLimitCoverId",
                table: "Limit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoverId",
                table: "Cover",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "BrokerPolicyTemplateCoverBrokerRefId",
                table: "Cover",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarRuleId",
                table: "CarRule",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "CarRuleCoverId1",
                table: "CarRule",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Car",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "BrokerPolicyTemplateCarBrokerRefId",
                table: "Car",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LimitRule",
                table: "LimitRule",
                column: "LimitRuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Limit",
                table: "Limit",
                column: "LimitId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Limit_LimitRuleCoverId",
                table: "Limit",
                column: "LimitRuleCoverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cover",
                table: "Cover",
                column: "CoverId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cover_LimitCoverId",
                table: "Cover",
                column: "LimitCoverId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Cover_QuestionCoverId",
                table: "Cover",
                column: "QuestionCoverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRule",
                table: "CarRule",
                column: "CarRuleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "CarId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Car_CarRuleCoverId",
                table: "Car",
                column: "CarRuleCoverId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BrokerPolicyTemplate_CarBrokerRefId",
                table: "BrokerPolicyTemplate",
                column: "CarBrokerRefId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BrokerPolicyTemplate_CoverBrokerRefId",
                table: "BrokerPolicyTemplate",
                column: "CoverBrokerRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CoverQuestionCoverId",
                table: "Question",
                column: "CoverQuestionCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_LimitRule_LimitRuleCoverId1",
                table: "LimitRule",
                column: "LimitRuleCoverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Limit_CoverLimitCoverId",
                table: "Limit",
                column: "CoverLimitCoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Cover_BrokerPolicyTemplateCoverBrokerRefId",
                table: "Cover",
                column: "BrokerPolicyTemplateCoverBrokerRefId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRule_CarRuleCoverId1",
                table: "CarRule",
                column: "CarRuleCoverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Car_BrokerPolicyTemplateCarBrokerRefId",
                table: "Car",
                column: "BrokerPolicyTemplateCarBrokerRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarType",
                table: "Car",
                column: "BrokerPolicyTemplateCarBrokerRefId",
                principalTable: "BrokerPolicyTemplate",
                principalColumn: "CarBrokerRefId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRuleType",
                table: "CarRule",
                column: "CarRuleCoverId1",
                principalTable: "Car",
                principalColumn: "CarRuleCoverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverType",
                table: "Cover",
                column: "BrokerPolicyTemplateCoverBrokerRefId",
                principalTable: "BrokerPolicyTemplate",
                principalColumn: "CoverBrokerRefId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitType",
                table: "Limit",
                column: "CoverLimitCoverId",
                principalTable: "Cover",
                principalColumn: "LimitCoverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitRuleType",
                table: "LimitRule",
                column: "LimitRuleCoverId1",
                principalTable: "Limit",
                principalColumn: "LimitRuleCoverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionType",
                table: "Question",
                column: "CoverQuestionCoverId",
                principalTable: "Cover",
                principalColumn: "QuestionCoverId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarType",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRuleType",
                table: "CarRule");

            migrationBuilder.DropForeignKey(
                name: "FK_CoverType",
                table: "Cover");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitType",
                table: "Limit");

            migrationBuilder.DropForeignKey(
                name: "FK_LimitRuleType",
                table: "LimitRule");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionType",
                table: "Question");

            migrationBuilder.DropIndex(
                name: "IX_Question_CoverQuestionCoverId",
                table: "Question");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LimitRule",
                table: "LimitRule");

            migrationBuilder.DropIndex(
                name: "IX_LimitRule_LimitRuleCoverId1",
                table: "LimitRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Limit",
                table: "Limit");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Limit_LimitRuleCoverId",
                table: "Limit");

            migrationBuilder.DropIndex(
                name: "IX_Limit_CoverLimitCoverId",
                table: "Limit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cover",
                table: "Cover");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cover_LimitCoverId",
                table: "Cover");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Cover_QuestionCoverId",
                table: "Cover");

            migrationBuilder.DropIndex(
                name: "IX_Cover_BrokerPolicyTemplateCoverBrokerRefId",
                table: "Cover");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRule",
                table: "CarRule");

            migrationBuilder.DropIndex(
                name: "IX_CarRule_CarRuleCoverId1",
                table: "CarRule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Car_CarRuleCoverId",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_BrokerPolicyTemplateCarBrokerRefId",
                table: "Car");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BrokerPolicyTemplate_CarBrokerRefId",
                table: "BrokerPolicyTemplate");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BrokerPolicyTemplate_CoverBrokerRefId",
                table: "BrokerPolicyTemplate");

            migrationBuilder.DropColumn(
                name: "CoverQuestionCoverId",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "LimitRuleId",
                table: "LimitRule");

            migrationBuilder.DropColumn(
                name: "LimitRuleCoverId1",
                table: "LimitRule");

            migrationBuilder.DropColumn(
                name: "LimitId",
                table: "Limit");

            migrationBuilder.DropColumn(
                name: "CoverLimitCoverId",
                table: "Limit");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Cover");

            migrationBuilder.DropColumn(
                name: "BrokerPolicyTemplateCoverBrokerRefId",
                table: "Cover");

            migrationBuilder.DropColumn(
                name: "CarRuleId",
                table: "CarRule");

            migrationBuilder.DropColumn(
                name: "CarRuleCoverId1",
                table: "CarRule");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "BrokerPolicyTemplateCarBrokerRefId",
                table: "Car");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LimitRule",
                table: "LimitRule",
                column: "LimitRuleCoverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Limit",
                table: "Limit",
                column: "LimitCoverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cover",
                table: "Cover",
                column: "CoverBrokerRefId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRule",
                table: "CarRule",
                column: "CarRuleCoverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "CarBrokerRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarType",
                table: "Car",
                column: "CarBrokerRefId",
                principalTable: "BrokerPolicyTemplate",
                principalColumn: "BrokerReferenceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRuleType",
                table: "CarRule",
                column: "CarRuleCoverId",
                principalTable: "Car",
                principalColumn: "CarBrokerRefId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverType",
                table: "Cover",
                column: "CoverBrokerRefId",
                principalTable: "BrokerPolicyTemplate",
                principalColumn: "BrokerReferenceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitType",
                table: "Limit",
                column: "LimitCoverId",
                principalTable: "Cover",
                principalColumn: "CoverBrokerRefId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LimitRuleType",
                table: "LimitRule",
                column: "LimitRuleCoverId",
                principalTable: "Limit",
                principalColumn: "LimitCoverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionType",
                table: "Question",
                column: "QuestionCoverId",
                principalTable: "Cover",
                principalColumn: "CoverBrokerRefId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
