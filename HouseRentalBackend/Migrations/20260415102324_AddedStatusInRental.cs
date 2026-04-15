using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentalBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusInRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Rentals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rentals");
        }
    }
}
