using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class ConfigureTodoList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_TodoList_TodoListEntityId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_TodoListEntityId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "TodoListEntityId",
                table: "Task");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoListEntityId",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_TodoListEntityId",
                table: "Task",
                column: "TodoListEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_TodoList_TodoListEntityId",
                table: "Task",
                column: "TodoListEntityId",
                principalTable: "TodoList",
                principalColumn: "Id");
        }
    }
}
