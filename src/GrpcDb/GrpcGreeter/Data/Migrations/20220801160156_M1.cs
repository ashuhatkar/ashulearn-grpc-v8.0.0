using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GrpcGreeter.Data.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "FirstName", "LastName", "School" },
                values: new object[,]
                {
                    { 1, "Ann", "Fox", "Nursing" },
                    { 2, "Sam", "Deo", "Mining" },
                    { 3, "Sue", "Cox", "Business" },
                    { 4, "Tim", "Lee", "Computing" },
                    { 5, "Jan", "Ray", "Nursing" },
                    { 6, "Ashish", "Hatkar", "Software" },
                    { 7, "Bhargav", "Hatkar", "Schooling" },
                    { 8, "Saee", "Hatkar", "Mothering" },
                    { 9, "Leena", "Hatkar", "Banking" },
                    { 10, "Janu", "KH", "Colleging" },
                    { 11, "Test", "Test", "Testing" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
