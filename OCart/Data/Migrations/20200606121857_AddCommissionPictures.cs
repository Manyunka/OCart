using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class AddCommissionPictures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommissionPictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommissionId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionPictures_Commissions_CommissionId",
                        column: x => x.CommissionId,
                        principalTable: "Commissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommissionPictures_CommissionId",
                table: "CommissionPictures",
                column: "CommissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommissionPictures");
        }
    }
}
