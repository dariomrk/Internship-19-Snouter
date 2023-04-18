using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Location_LocationId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Location_LocationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Users",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                newName: "IX_Users_CityId");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Product",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_LocationId",
                table: "Product",
                newName: "IX_Product_CityId");

            migrationBuilder.AddColumn<int>(
                name: "PreciseLocationId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreciseLocationId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CountyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreciseLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    LocationType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreciseLocation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PreciseLocationId",
                table: "Users",
                column: "PreciseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_PreciseLocationId",
                table: "Product",
                column: "PreciseLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_City_CountyId",
                table: "City",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_City_CityId",
                table: "Product",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_PreciseLocation_PreciseLocationId",
                table: "Product",
                column: "PreciseLocationId",
                principalTable: "PreciseLocation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_City_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PreciseLocation_PreciseLocationId",
                table: "Users",
                column: "PreciseLocationId",
                principalTable: "PreciseLocation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_City_CityId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_PreciseLocation_PreciseLocationId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_City_CityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PreciseLocation_PreciseLocationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "PreciseLocation");

            migrationBuilder.DropIndex(
                name: "IX_Users_PreciseLocationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Product_PreciseLocationId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PreciseLocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PreciseLocationId",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Users",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CityId",
                table: "Users",
                newName: "IX_Users_LocationId");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Product",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_CityId",
                table: "Product",
                newName: "IX_Product_LocationId");

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountyId = table.Column<int>(type: "integer", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    LocationType = table.Column<int>(type: "integer", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_County_CountyId",
                        column: x => x.CountyId,
                        principalTable: "County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_CountyId",
                table: "Location",
                column: "CountyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Location_LocationId",
                table: "Product",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Location_LocationId",
                table: "Users",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
