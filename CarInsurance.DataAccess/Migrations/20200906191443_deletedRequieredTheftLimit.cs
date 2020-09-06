using Microsoft.EntityFrameworkCore.Migrations;

namespace CarInsurance.DataAccess.Migrations
{
    public partial class deletedRequieredTheftLimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LimitValues",
                table: "TheftLimit",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LimitValues",
                table: "TheftLimit",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
