using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HermesChat_TeamA.Migrations
{
    /// <inheritdoc />
    public partial class AddModelsToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupConversation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupConversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    receiverId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SentDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrivateConversation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateConversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupConversation");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "PrivateConversation");

            migrationBuilder.DropTable(
                name: "UserGroup");
        }
    }
}
