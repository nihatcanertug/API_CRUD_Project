using Microsoft.EntityFrameworkCore.Migrations;

namespace API_CRUD_Project.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boxers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Division = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Boxers",
                columns: new[] { "Id", "Alias", "Division", "FullName", "Status" },
                values: new object[,]
                {
                    { 1, "King In the Ring", "Heavyweight", "Muhammed Ali", "Retired" },
                    { 2, "Iron", "Heavyweight", "Mike Tyson", "Retired" },
                    { 3, "Lioness", "Heavyweight", "Lenox Lewis", "Retired" },
                    { 4, "Real Deal", "Heavyweight", "Evander Holyfiled", "Retired" },
                    { 5, "Savage", "Heavyweight", "Rock Marciano", "Retired" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Boxers");
        }
    }
}
