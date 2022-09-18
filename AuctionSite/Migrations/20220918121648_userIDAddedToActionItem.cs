using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionSite.Migrations
{
    public partial class userIDAddedToActionItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "AuctionItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_UserID",
                table: "AuctionItems",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionItems_AspNetUsers_UserID",
                table: "AuctionItems",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionItems_AspNetUsers_UserID",
                table: "AuctionItems");

            migrationBuilder.DropIndex(
                name: "IX_AuctionItems_UserID",
                table: "AuctionItems");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "AuctionItems");
        }
    }
}
