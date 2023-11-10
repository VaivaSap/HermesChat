using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HermesChat_TeamA.Migrations
{
    /// <inheritdoc />
    public partial class fixuserprofilepicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilePictures_AspNetUsers_UserId",
                table: "UserProfilePictures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfilePictures_AspNetUsers_UserId",
                table: "UserProfilePictures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfilePictures",
                table: "UserProfilePictures");

            migrationBuilder.RenameTable(
                name: "UserProfilePictures",
                newName: "UserProfilePicture");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfilePictures_UserId",
                table: "UserProfilePicture",
                newName: "IX_UserProfilePicture_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfilePicture",
                table: "UserProfilePicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilePicture_AspNetUsers_UserId",
                table: "UserProfilePicture",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
