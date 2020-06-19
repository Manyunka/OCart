using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class FixAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishedBet",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Story",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "InitialBet",
                table: "Auctions",
                newName: "InitialCostBet");

            migrationBuilder.AddColumn<DateTime>(
                name: "Finished",
                table: "Auctions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "WinBetId",
                table: "Auctions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuctionId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_WinBetId",
                table: "Auctions",
                column: "WinBetId",
                unique: true,
                filter: "[WinBetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_AuctionId",
                table: "Bets",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_CreatorId",
                table: "Bets",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Bets_WinBetId",
                table: "Auctions",
                column: "WinBetId",
                principalTable: "Bets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Bets_WinBetId",
                table: "Auctions");

            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_WinBetId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "WinBetId",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "InitialCostBet",
                table: "Auctions",
                newName: "InitialBet");

            migrationBuilder.AddColumn<decimal>(
                name: "FinishedBet",
                table: "Auctions",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Story",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
