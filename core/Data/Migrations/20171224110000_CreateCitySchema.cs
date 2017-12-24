using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochka.Data.Migrations
{
    public partial class CreateCitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsRepresentation = table.Column<bool>(nullable: false),
                    LatinName = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_LatinName",
                table: "Cities",
                column: "LatinName",
                unique: true);

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] {"Name", "LatinName", "IsRepresentation"},
                values: new object[] {"Москва", "Moskva", true});
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Санкт-Петербург ", "Sankt-Peterburg", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Новосибирск", "Novosibirsk", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Екатеринбург", "Ekaterinburg", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Нижний Новгород", "Nizhnij Novgorod", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Казань", "Kazan", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Челябинск", "Chelyabinsk", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Омск", "Omsk", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Самара", "Samara", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Ростов-на-Дону", "Rostov-na-Donu", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Уфа", "Ufa", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Красноярск", "Krasnoyarsk", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Пермь", "Perm", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Воронеж", "Voronezh", true });
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new string[] { "Name", "LatinName", "IsRepresentation" },
                values: new object[] { "Волгоград", "Volgograd", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
