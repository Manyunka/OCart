using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class FixMoneyFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialBet",
                table: "Activities");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Activities",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InitialBetCost",
                table: "Activities",
                type: "Money",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitialBetCost",
                table: "Activities");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Activities",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "InitialBet",
                table: "Activities",
                type: "decimal(18,4)",
                nullable: true);
        }
    }
}
