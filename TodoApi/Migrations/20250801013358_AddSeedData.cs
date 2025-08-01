using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoItemDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItemDTO", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TodoItemDTO",
                columns: new[] { "Id", "IsComplete", "Name" },
                values: new object[,]
                {
                    { 1, true, "Do the thing" },
                    { 2, false, "Do the thing" },
                    { 3, true, "Do the thing" },
                    { 4, false, "Do the thing" },
                    { 5, false, "Do the thing" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItemDTO");
        }
    }
}
