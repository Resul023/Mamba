using Microsoft.EntityFrameworkCore.Migrations;

namespace Mamba.Migrations
{
    public partial class CreateTableTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    TwitUrl = table.Column<string>(nullable: true),
                    FaceUrl = table.Column<string>(nullable: true),
                    InstaUrl = table.Column<string>(nullable: true),
                    LikdnUrl = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
