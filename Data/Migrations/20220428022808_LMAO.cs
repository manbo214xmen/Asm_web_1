using Microsoft.EntityFrameworkCore.Migrations;

namespace Asm_web_1.Data.Migrations
{
    public partial class LMAO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    info = table.Column<string>(nullable: true),
                    bookquantity = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    cataid = table.Column<int>(nullable: false),
                    author = table.Column<string>(nullable: true),
                    imgfile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
