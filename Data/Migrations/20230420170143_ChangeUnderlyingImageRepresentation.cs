using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUnderlyingImageRepresentation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64",
                table: "Image",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64",
                table: "Image");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Image",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
