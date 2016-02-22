using System;
using Microsoft.Data.Entity.Migrations;

namespace StackLite.Core.Persistance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AnsweredBy = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Votes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerData", x => x.Id);
                });
            migrationBuilder.CreateTable(
                name: "QuestionData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AskedByUserName = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AnswerData");
            migrationBuilder.DropTable("QuestionData");
        }
    }
}
