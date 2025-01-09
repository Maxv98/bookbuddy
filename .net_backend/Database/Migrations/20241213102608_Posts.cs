using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Posts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BookBuddies_BookBuddyId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookBuddies",
                table: "BookBuddies");

            migrationBuilder.RenameTable(
                name: "BookBuddies",
                newName: "Bookbuddies");

            migrationBuilder.RenameColumn(
                name: "BookBuddyId",
                table: "Posts",
                newName: "BookbuddyId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BookBuddyId",
                table: "Posts",
                newName: "IX_Posts_BookbuddyId");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Bookbuddies",
                newName: "Username");

            migrationBuilder.RenameIndex(
                name: "IX_BookBuddies_UserName",
                table: "Bookbuddies",
                newName: "IX_Bookbuddies_Username");

            migrationBuilder.RenameIndex(
                name: "IX_BookBuddies_Email",
                table: "Bookbuddies",
                newName: "IX_Bookbuddies_Email");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookbuddies",
                table: "Bookbuddies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BookbuddySavedPosts",
                columns: table => new
                {
                    SavedByBookbuddiesId = table.Column<int>(type: "int", nullable: false),
                    SavedPostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookbuddySavedPosts", x => new { x.SavedByBookbuddiesId, x.SavedPostsId });
                    table.ForeignKey(
                        name: "FK_BookbuddySavedPosts_Bookbuddies_SavedByBookbuddiesId",
                        column: x => x.SavedByBookbuddiesId,
                        principalTable: "Bookbuddies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookbuddySavedPosts_Posts_SavedPostsId",
                        column: x => x.SavedPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookbuddySavedPosts_SavedPostsId",
                table: "BookbuddySavedPosts",
                column: "SavedPostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Bookbuddies_BookbuddyId",
                table: "Posts",
                column: "BookbuddyId",
                principalTable: "Bookbuddies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Bookbuddies_BookbuddyId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "BookbuddySavedPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookbuddies",
                table: "Bookbuddies");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Bookbuddies",
                newName: "BookBuddies");

            migrationBuilder.RenameColumn(
                name: "BookbuddyId",
                table: "Posts",
                newName: "BookBuddyId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BookbuddyId",
                table: "Posts",
                newName: "IX_Posts_BookBuddyId");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "BookBuddies",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Bookbuddies_Username",
                table: "BookBuddies",
                newName: "IX_BookBuddies_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Bookbuddies_Email",
                table: "BookBuddies",
                newName: "IX_BookBuddies_Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookBuddies",
                table: "BookBuddies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BookBuddies_BookBuddyId",
                table: "Posts",
                column: "BookBuddyId",
                principalTable: "BookBuddies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
