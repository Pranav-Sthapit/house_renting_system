using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentalBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialPhaseForRecommendationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RenterBehaviourWithProperties",
                columns: table => new
                {
                    RenterId = table.Column<int>(type: "integer", nullable: false),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    TimesViewed = table.Column<int>(type: "integer", nullable: false),
                    Applied = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenterBehaviourWithProperties", x => new { x.RenterId, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_RenterBehaviourWithProperties_Person_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenterBehaviourWithProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenterBehaviourWithProperties_PropertyId",
                table: "RenterBehaviourWithProperties",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenterBehaviourWithProperties");
        }
    }
}
