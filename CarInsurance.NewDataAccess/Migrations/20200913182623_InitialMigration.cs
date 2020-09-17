using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInsurance.NewDataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrokerPolicyTemplate",
                columns: table => new
                {
                    BrokerReferenceId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(type: "date", nullable: false),
                    CarBrokerRefId = table.Column<Guid>(nullable: false),
                    CoverBrokerRefId = table.Column<Guid>(nullable: false),
                    TemplateReady = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerPolicyTemplate", x => x.BrokerReferenceId);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    CarBrokerRefId = table.Column<Guid>(nullable: false),
                    Brand = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Model = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Year = table.Column<DateTime>(type: "date", nullable: false),
                    EngineCC = table.Column<short>(type: "smallint", nullable: false),
                    EuroStandard = table.Column<string>(unicode: false, maxLength: 8, nullable: false),
                    CarRuleCoverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.CarBrokerRefId);
                    table.CheckConstraint("constraint_euroStandard", "EuroStandard in ('Euro1','Euro2','Euro3','Euro4','Euro5','Euro6')");
                    table.CheckConstraint("constraint_carBrand", "Brand in ('volkswagen','bmw','mercedes-benz','opel','dacia','renault','toyota','skoda')");
                    table.CheckConstraint("constraint_carEngine", "EngineCC > 1000 and EngineCC < 5000");
                    table.CheckConstraint("constraint_carMode", "Model in ('octavia','rapid','fabia','passat','golf','c200','e200','c220','e220','320d','325d','330d','530d','520d','525d','astra','corsa','megane','clio','logan','sandero','avensis','prius')");
                    table.ForeignKey(
                        name: "FK_CarType",
                        column: x => x.CarBrokerRefId,
                        principalTable: "BrokerPolicyTemplate",
                        principalColumn: "BrokerReferenceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cover",
                columns: table => new
                {
                    CoverBrokerRefId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    AdditionDate = table.Column<DateTime>(type: "date", nullable: false),
                    QuestionCoverId = table.Column<Guid>(nullable: false),
                    LimitCoverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cover", x => x.CoverBrokerRefId);
                    table.CheckConstraint("constraint_coverType", "Type in ('theft','accidents','naturalhazard')");
                    table.ForeignKey(
                        name: "FK_CoverType",
                        column: x => x.CoverBrokerRefId,
                        principalTable: "BrokerPolicyTemplate",
                        principalColumn: "BrokerReferenceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarRule",
                columns: table => new
                {
                    CarRuleCoverId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Multiplier = table.Column<double>(type: "float(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarRule", x => x.CarRuleCoverId);
                    table.ForeignKey(
                        name: "FK_CarRuleType",
                        column: x => x.CarRuleCoverId,
                        principalTable: "Car",
                        principalColumn: "CarBrokerRefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Limit",
                columns: table => new
                {
                    LimitCoverId = table.Column<Guid>(nullable: false),
                    CoverType = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    LimitValues = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    LimitRuleCoverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limit", x => x.LimitCoverId);
                    table.ForeignKey(
                        name: "FK_LimitType",
                        column: x => x.LimitCoverId,
                        principalTable: "Cover",
                        principalColumn: "CoverBrokerRefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionCoverId = table.Column<Guid>(nullable: false),
                    CoverType = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 25, nullable: false),
                    Text = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    QuestionRuleCoverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionCoverId);
                    table.ForeignKey(
                        name: "FK_QuestionType",
                        column: x => x.QuestionCoverId,
                        principalTable: "Cover",
                        principalColumn: "CoverBrokerRefId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LimitRule",
                columns: table => new
                {
                    LimitRuleCoverId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    RuleText = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Multiplier = table.Column<double>(type: "float(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimitRule", x => x.LimitRuleCoverId);
                    table.ForeignKey(
                        name: "FK_LimitRuleType",
                        column: x => x.LimitRuleCoverId,
                        principalTable: "Limit",
                        principalColumn: "LimitCoverId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionRule",
                columns: table => new
                {
                    QuestionRuleCoverId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    RuleText = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Multiplier = table.Column<double>(type: "float(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionRule", x => x.QuestionRuleCoverId);
                    table.ForeignKey(
                        name: "FK_QuestionRuleType",
                        column: x => x.QuestionRuleCoverId,
                        principalTable: "Question",
                        principalColumn: "QuestionCoverId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarRule");

            migrationBuilder.DropTable(
                name: "LimitRule");

            migrationBuilder.DropTable(
                name: "QuestionRule");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Limit");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Cover");

            migrationBuilder.DropTable(
                name: "BrokerPolicyTemplate");
        }
    }
}
