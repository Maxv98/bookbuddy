using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BookBuddyPostRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_BookBuddyId",
                table: "Posts",
                column: "BookBuddyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BookBuddies_BookBuddyId",
                table: "Posts",
                column: "BookBuddyId",
                principalTable: "BookBuddies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BookBuddies_BookBuddyId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_BookBuddyId",
                table: "Posts");
        }
    }
}
