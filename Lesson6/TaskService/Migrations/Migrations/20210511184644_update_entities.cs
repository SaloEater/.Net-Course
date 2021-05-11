using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrations.Migrations
{
    public partial class update_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tasks_texts_TaskId_TextId",
                table: "tasks_texts");

            migrationBuilder.AddColumn<Guid>(
                name: "WordId",
                table: "tasks_texts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "word",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastSavedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastSavedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_word", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tasks_texts_TaskId_TextId_WordId",
                table: "tasks_texts",
                columns: new[] { "TaskId", "TextId", "WordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_texts_WordId",
                table: "tasks_texts",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_texts_word_WordId",
                table: "tasks_texts",
                column: "WordId",
                principalTable: "word",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_texts_word_WordId",
                table: "tasks_texts");

            migrationBuilder.DropTable(
                name: "word");

            migrationBuilder.DropIndex(
                name: "IX_tasks_texts_TaskId_TextId_WordId",
                table: "tasks_texts");

            migrationBuilder.DropIndex(
                name: "IX_tasks_texts_WordId",
                table: "tasks_texts");

            migrationBuilder.DropColumn(
                name: "WordId",
                table: "tasks_texts");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_texts_TaskId_TextId",
                table: "tasks_texts",
                columns: new[] { "TaskId", "TextId" },
                unique: true);
        }
    }
}
