using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentalBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedRentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rent",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rent",
                table: "Properties");
        }
    }
}
