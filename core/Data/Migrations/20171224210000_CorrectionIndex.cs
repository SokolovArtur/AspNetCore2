using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochka.Data.Migrations
{
    public partial class CorrectionIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vacancies_LatinName",
                table: "Vacancies");

            migrationBuilder.CreateIndex(
                name: "IX_VacanciesCities_VacancyId_CityId",
                table: "VacanciesCities",
                columns: new[] { "VacancyId", "CityId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VacanciesCities_VacancyId_CityId",
                table: "VacanciesCities");
            
            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_LatinName",
                table: "Vacancies",
                column: "LatinName",
                unique: true);
        }
    }
}
