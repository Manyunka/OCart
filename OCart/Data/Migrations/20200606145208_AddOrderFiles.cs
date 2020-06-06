using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class AddOrderFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionOrderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuctionOrderMessageId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionOrderFiles_AuctionOrdersMessages_AuctionOrderMessageId",
                        column: x => x.AuctionOrderMessageId,
                        principalTable: "AuctionOrdersMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommissionOrderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommissionOrderMessageId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionOrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionOrderFiles_CommissionOrdersMessages_CommissionOrderMessageId",
                        column: x => x.CommissionOrderMessageId,
                        principalTable: "CommissionOrdersMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrderFiles_AuctionOrderMessageId",
                table: "AuctionOrderFiles",
                column: "AuctionOrderMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrderFiles_CommissionOrderMessageId",
                table: "CommissionOrderFiles",
                column: "CommissionOrderMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionOrderFiles");

            migrationBuilder.DropTable(
                name: "CommissionOrderFiles");
        }
    }
}
