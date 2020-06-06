using Microsoft.EntityFrameworkCore.Migrations;

namespace OCart.Data.Migrations
{
    public partial class FixMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_AspNetUsers_CreatorId",
                table: "Dialogs");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_CreatorId",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Dialogs");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Messages",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InterlocutorId",
                table: "Dialogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatorId",
                table: "Messages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_InterlocutorId",
                table: "Dialogs",
                column: "InterlocutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_AspNetUsers_InterlocutorId",
                table: "Dialogs",
                column: "InterlocutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_CreatorId",
                table: "Messages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dialogs_AspNetUsers_InterlocutorId",
                table: "Dialogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_CreatorId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_CreatorId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Dialogs_InterlocutorId",
                table: "Dialogs");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "InterlocutorId",
                table: "Dialogs");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Dialogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Dialogs_CreatorId",
                table: "Dialogs",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dialogs_AspNetUsers_CreatorId",
                table: "Dialogs",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
