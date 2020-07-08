using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class FixOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommissionOrders_Activities_CommissionId",
                table: "CommissionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CommissionOrders_AspNetUsers_CustomerId",
                table: "CommissionOrders");

            migrationBuilder.DropTable(
                name: "AuctionOrderFiles");

            migrationBuilder.DropTable(
                name: "CommissionOrderFiles");

            migrationBuilder.DropTable(
                name: "AuctionOrdersMessages");

            migrationBuilder.DropTable(
                name: "CommissionOrdersMessages");

            migrationBuilder.DropTable(
                name: "AuctionOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommissionOrders",
                table: "CommissionOrders");

            migrationBuilder.RenameTable(
                name: "CommissionOrders",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionOrders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionOrders_CommissionId",
                table: "Orders",
                newName: "IX_Orders_CommissionId");

            migrationBuilder.AddColumn<Guid>(
                name: "AuctionId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Orders",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrdersMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    CreatorId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersMessages_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersMessages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderMessageId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderFiles_OrdersMessages_OrderMessageId",
                        column: x => x.OrderMessageId,
                        principalTable: "OrdersMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AuctionId",
                table: "Orders",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderFiles_OrderMessageId",
                table: "OrderFiles",
                column: "OrderMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersMessages_CreatorId",
                table: "OrdersMessages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersMessages_OrderId",
                table: "OrdersMessages",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Activities_AuctionId",
                table: "Orders",
                column: "AuctionId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Activities_CommissionId",
                table: "Orders",
                column: "CommissionId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Activities_AuctionId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Activities_CommissionId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderFiles");

            migrationBuilder.DropTable(
                name: "OrdersMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AuctionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AuctionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "CommissionOrders");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "CommissionOrders",
                newName: "IX_CommissionOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CommissionId",
                table: "CommissionOrders",
                newName: "IX_CommissionOrders_CommissionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommissionOrders",
                table: "CommissionOrders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AuctionOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionOrders_Activities_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AuctionOrders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommissionOrdersMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommissionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AuctionOrdersMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "CommissionOrderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommissionOrderMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AuctionOrderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AuctionOrderMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrderFiles_AuctionOrderMessageId",
                table: "AuctionOrderFiles",
                column: "AuctionOrderMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrders_AuctionId",
                table: "AuctionOrders",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrders_CustomerId",
                table: "AuctionOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrdersMessages_AuctionOrderId",
                table: "AuctionOrdersMessages",
                column: "AuctionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionOrdersMessages_CreatorId",
                table: "AuctionOrdersMessages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrderFiles_CommissionOrderMessageId",
                table: "CommissionOrderFiles",
                column: "CommissionOrderMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrdersMessages_CommissionOrderId",
                table: "CommissionOrdersMessages",
                column: "CommissionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrdersMessages_CreatorId",
                table: "CommissionOrdersMessages",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionOrders_Activities_CommissionId",
                table: "CommissionOrders",
                column: "CommissionId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionOrders_AspNetUsers_CustomerId",
                table: "CommissionOrders",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
