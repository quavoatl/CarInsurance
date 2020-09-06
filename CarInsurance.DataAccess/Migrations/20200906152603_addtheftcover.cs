using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInsurance.DataAccess.Migrations
{
    public partial class addtheftcover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TheftLimit",
                columns: table => new
                {
                    ThefLimittBrokerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LimitValues = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheftLimit", x => x.ThefLimittBrokerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheftLimit");
        }
    }
}
