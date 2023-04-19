using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCroatiaSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Croatia" });

            migrationBuilder.InsertData(
                table: "County",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Bjelovar-Bilogora" },
                    { 2, 1, "Brod-Posavina" },
                    { 3, 1, "Dubrovnik-Neretva" },
                    { 4, 1, "Istria" },
                    { 5, 1, "Karlovac" },
                    { 6, 1, "Koprivnica-Krizevci" },
                    { 7, 1, "Krapina-Zagorje" },
                    { 8, 1, "Lika-Senj" },
                    { 9, 1, "Medimurje" },
                    { 10, 1, "Osijek-Baranja" },
                    { 11, 1, "Pozega-Slavonia" },
                    { 12, 1, "Primorje-Gorski Kotar" },
                    { 13, 1, "Sisak-Moslavina" },
                    { 14, 1, "Split-Dalmatia" },
                    { 15, 1, "Sibenik-Knin" },
                    { 16, 1, "Varazdin" },
                    { 17, 1, "Virovitica-Podravina" },
                    { 18, 1, "Vukovar-Srijem" },
                    { 19, 1, "Zadar" },
                    { 20, 1, "Zagreb" },
                    { 21, 1, "City of Zagreb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "County",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Country",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
