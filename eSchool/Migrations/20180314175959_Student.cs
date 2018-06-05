using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace eSchool.Migrations
{
    public partial class Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<int>(nullable: false),
                    Class_Id = table.Column<int>(nullable: false),
                    Created_At = table.Column<DateTime>(nullable: false),
                    Created_By = table.Column<DateTime>(nullable: false),
                    Parent_Email = table.Column<string>(nullable: false),
                    Stud_Email = table.Column<string>(nullable: false),
                    Updated_At = table.Column<DateTime>(nullable: false),
                    Updated_By = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");
        }
    }
}
