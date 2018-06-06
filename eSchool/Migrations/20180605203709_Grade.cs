using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchool.Migrations
{
    public partial class Grade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "ExamResult",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<int>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Created_By = table.Column<string>(nullable: true),
                    Grade_letter = table.Column<string>(maxLength: 2, nullable: false),
                    Grade_number = table.Column<int>(maxLength: 2, nullable: false),
                    Teacher_Id = table.Column<string>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamResult_GradeId",
                table: "ExamResult",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_UserId",
                table: "Grade",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResult_Grade_GradeId",
                table: "ExamResult",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResult_Grade_GradeId",
                table: "ExamResult");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_ExamResult_GradeId",
                table: "ExamResult");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "ExamResult");
        }
    }
}
