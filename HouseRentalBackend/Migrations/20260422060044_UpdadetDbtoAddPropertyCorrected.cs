using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentalBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdadetDbtoAddPropertyCorrected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPicture_Properties_PropertyId",
                table: "PropertyPicture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyPicture",
                table: "PropertyPicture");

            migrationBuilder.RenameTable(
                name: "PropertyPicture",
                newName: "PropertyPictureList");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyPicture_PropertyId",
                table: "PropertyPictureList",
                newName: "IX_PropertyPictureList_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyPictureList",
                table: "PropertyPictureList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPictureList_Properties_PropertyId",
                table: "PropertyPictureList",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPictureList_Properties_PropertyId",
                table: "PropertyPictureList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyPictureList",
                table: "PropertyPictureList");

            migrationBuilder.RenameTable(
                name: "PropertyPictureList",
                newName: "PropertyPicture");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyPictureList_PropertyId",
                table: "PropertyPicture",
                newName: "IX_PropertyPicture_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyPicture",
                table: "PropertyPicture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPicture_Properties_PropertyId",
                table: "PropertyPicture",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
