using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordBackup.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    ChannelId = table.Column<ulong>(nullable: false),
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Url = table.Column<string>(nullable: true),
                    ProxyUrl = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Width = table.Column<uint>(nullable: false),
                    Height = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    time_string = table.Column<string>(nullable: true),
                    ChannelName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    IsTTS = table.Column<bool>(nullable: false),
                    IsPinned = table.Column<bool>(nullable: false),
                    ChannelId = table.Column<ulong>(nullable: false),
                    UserId = table.Column<ulong>(nullable: false),
                    time = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
