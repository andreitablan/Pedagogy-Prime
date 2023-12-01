using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedagogyPrime.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCoverageEntityAndCourseVisibilitiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coverage",
                table: "Courses");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleForStudents",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Coverages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Precentage = table.Column<double>(type: "float", nullable: false),
                    GoodWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadWords = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coverages_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coverages_CourseId",
                table: "Coverages",
                column: "CourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coverages");

            migrationBuilder.DropColumn(
                name: "IsVisibleForStudents",
                table: "Courses");

            migrationBuilder.AddColumn<double>(
                name: "Coverage",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
