using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HermesChat_TeamA.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationUser_AspNetUsers_UserId",
                table: "ConversationUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationUser_AspNetUsers_UserId1",
                table: "ConversationUser");

            migrationBuilder.DropIndex(
                name: "IX_ConversationUser_UserId1",
                table: "ConversationUser");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ConversationUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConversationUser",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationUser_AspNetUsers_UserId",
                table: "ConversationUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationUser_AspNetUsers_UserId",
                table: "ConversationUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConversationUser",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ConversationUser",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConversationUser_UserId1",
                table: "ConversationUser",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationUser_AspNetUsers_UserId",
                table: "ConversationUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationUser_AspNetUsers_UserId1",
                table: "ConversationUser",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
