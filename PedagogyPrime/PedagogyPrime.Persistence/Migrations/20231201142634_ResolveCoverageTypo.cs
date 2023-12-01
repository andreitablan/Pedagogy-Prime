using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PedagogyPrime.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ResolveCoverageTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Precentage",
                table: "Coverages",
                newName: "Percentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "Coverages",
                newName: "Precentage");
        }
    }
}
