using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class Task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfTasks",
                table: "TodoLists");

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                    TodoListEntityId = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_TodoLists_TodoListEntityId",
                        column: x => x.TodoListEntityId,
                        principalTable: "TodoLists",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDone = table.Column<bool>(type: "bit", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_TodoListEntityId",
                table: "Task",
                column: "TodoListEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTasks",
                table: "TodoLists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
