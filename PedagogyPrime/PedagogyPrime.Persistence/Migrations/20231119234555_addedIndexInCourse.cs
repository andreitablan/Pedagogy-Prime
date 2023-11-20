using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedagogyPrime.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedIndexInCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                table: "Courses");
        }
    }
}
