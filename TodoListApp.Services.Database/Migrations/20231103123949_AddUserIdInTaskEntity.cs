using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations
{
    public partial class AddUserIdInTaskEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_User_UserEntityId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_UserEntityId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Task");

            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "Task",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserEntityId",
                table: "Task",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_User_UserEntityId",
                table: "Task",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
