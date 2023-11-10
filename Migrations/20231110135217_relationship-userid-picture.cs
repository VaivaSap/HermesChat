using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HermesChat_TeamA.Migrations
{
    /// <inheritdoc />
    public partial class relationshipuseridpicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfilePictures_AspNetUsers_UserId",
                table: "UserProfilePictures");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserProfilePictures_UserId",
            //    table: "UserProfilePictures");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProfilePictures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePictures_UserId",
                table: "UserProfilePictures",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilePictures_AspNetUsers_UserId",
                table: "UserProfilePictures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfilePictures_AspNetUsers_UserId",
                table: "UserProfilePictures");

            migrationBuilder.DropIndex(
                name: "IX_UserProfilePictures_UserId",
                table: "UserProfilePictures");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserProfilePictures",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfilePictures_UserId",
                table: "UserProfilePictures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfilePictures_AspNetUsers_UserId",
                table: "UserProfilePictures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
