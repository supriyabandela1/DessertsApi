using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DessertsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageDesktop",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageMobile",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageTablet",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ImageThumbnail",
                table: "Products",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImageThumbnail");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageDesktop",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageMobile",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageTablet",
                table: "Products",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
