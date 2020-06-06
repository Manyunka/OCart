using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class AddCommissionOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommissionOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommissionId = table.Column<Guid>(nullable: true),
                    CustomerId = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionOrders_Commissions_CommissionId",
                        column: x => x.CommissionId,
                        principalTable: "Commissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CommissionOrders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrders_CommissionId",
                table: "CommissionOrders",
                column: "CommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionOrders_CustomerId",
                table: "CommissionOrders",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommissionOrders");
        }
    }
}
