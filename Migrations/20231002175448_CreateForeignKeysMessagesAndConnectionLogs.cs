using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HermesChat_TeamA.Migrations
{
    /// <inheritdoc />
    public partial class CreateForeignKeysMessagesAndConnectionLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConnectedUser",
                table: "ConnectionLog",
                newName: "ConnectedUserId");

            migrationBuilder.AddColumn<int>(
                name: "ConversationId1",
                table: "Message",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ConnectionLog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationId1",
                table: "Message",
                column: "ConversationId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionLog_UserId",
                table: "ConnectionLog",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConnectionLog_AspNetUsers_UserId",
                table: "ConnectionLog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_GroupConversation_ConversationId1",
                table: "Message",
                column: "ConversationId1",
                principalTable: "GroupConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConnectionLog_AspNetUsers_UserId",
                table: "ConnectionLog");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_GroupConversation_ConversationId1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ConversationId1",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_ConnectionLog_UserId",
                table: "ConnectionLog");

            migrationBuilder.DropColumn(
                name: "ConversationId1",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ConnectionLog");

            migrationBuilder.RenameColumn(
                name: "ConnectedUserId",
                table: "ConnectionLog",
                newName: "ConnectedUser");
        }
    }
}
