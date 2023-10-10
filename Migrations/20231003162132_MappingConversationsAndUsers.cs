using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HermesChat_TeamA.Migrations
{
    /// <inheritdoc />
    public partial class MappingConversationsAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_GroupConversation_ConversationId1",
                table: "Message");

            migrationBuilder.DropTable(
                name: "GroupConversation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateConversation",
                table: "PrivateConversation");

            migrationBuilder.RenameTable(
                name: "PrivateConversation",
                newName: "Conversation");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Conversation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Conversation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ConversationUser",
                columns: table => new
                {
                    ConversationsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationUser", x => new { x.ConversationsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ConversationUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationUser_Conversation_ConversationsId",
                        column: x => x.ConversationsId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationUser_UsersId",
                table: "ConversationUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversation_ConversationId1",
                table: "Message",
                column: "ConversationId1",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversation_ConversationId1",
                table: "Message");

            migrationBuilder.DropTable(
                name: "ConversationUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Conversation");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "PrivateConversation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateConversation",
                table: "PrivateConversation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GroupConversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupConversation", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Message_GroupConversation_ConversationId1",
                table: "Message",
                column: "ConversationId1",
                principalTable: "GroupConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
