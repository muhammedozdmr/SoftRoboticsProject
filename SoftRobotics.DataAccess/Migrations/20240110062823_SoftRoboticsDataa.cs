using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftRobotics.DataAccess.Migrations
{
    public partial class SoftRoboticsDataa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RandomWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountWord = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RandomWord", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "RandomWord",
                columns: new[] { "Id", "CountWord", "Word" },
                values: new object[] { 1, 7, "AbCdEfG" });

            migrationBuilder.InsertData(
                table: "RandomWord",
                columns: new[] { "Id", "CountWord", "Word" },
                values: new object[] { 2, 12, "Test2AbCdEfG" });

            migrationBuilder.InsertData(
                table: "RandomWord",
                columns: new[] { "Id", "CountWord", "Word" },
                values: new object[] { 3, 13, "TeStUcAbCdEfG" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RandomWord");
        }
    }
}
