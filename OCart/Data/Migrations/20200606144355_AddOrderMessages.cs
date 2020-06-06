using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class AddOrderMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuctionOrdersMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AuctionOrderId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionOrdersMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionOrdersMessages_AuctionOrders_AuctionOrderId",
                        column: x => x.AuctionOrderId,
                        principalTable: "AuctionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionOrdersMessages_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommissionOrdersMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommissionOrderId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionOrdersMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionOrdersMessages_CommissionOrders_CommissionOrderId",
                        column: x => x.CommissionOrderId,
                        principalTable: "CommissionOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommissionOrdersMessages_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrdersMessages_AuctionOrderId",
                table: "AuctionOrdersMessages",
                column: "AuctionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrdersMessages_CreatorId",
                table: "AuctionOrdersMessages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrdersMessages_CommissionOrderId",
                table: "CommissionOrdersMessages",
                column: "CommissionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrdersMessages_CreatorId",
                table: "CommissionOrdersMessages",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionOrdersMessages");

            migrationBuilder.DropTable(
                name: "CommissionOrdersMessages");
        }
    }
}
