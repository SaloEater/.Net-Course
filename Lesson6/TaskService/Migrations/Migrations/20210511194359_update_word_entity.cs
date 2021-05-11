using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class update_word_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TaskId1",
                table: "word",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_word_TaskId1",
                table: "word",
                column: "TaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_word_task_TaskId1",
                table: "word",
                column: "TaskId1",
                principalTable: "task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_word_task_TaskId1",
                table: "word");

            migrationBuilder.DropIndex(
                name: "IX_word_TaskId1",
                table: "word");

            migrationBuilder.DropColumn(
                name: "TaskId1",
                table: "word");
        }
    }
}
