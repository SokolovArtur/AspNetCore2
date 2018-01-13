using Microsoft.EntityFrameworkCore.Migrations;

namespace Tochka.Data.Migrations
{
    public partial class CreateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new string[]
                {
                    "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled",
                    "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber",
                    "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
                },
                values: new object[]
                {
                    "cf00e896-a8bd-4cde-a066-e1852fc950d6", 0, "b3f45864-15a5-4da2-9163-89c4ec8950f4", "root@localhost",
                    true, false, null, "ROOT@LOCALHOST", "ROOT",
                    "AQAAAAEAACcQAAAAEChycrQEJFhfFQp6OBSpvDjMa7+1yjJkSwOwURaGhaG+wm5WkWPOTkVOejmv5pg4Wg==", null, false,
                    "d3af4a4e-2a97-4ebd-bc03-8c0676eebe87", false, "root"
                });
            
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new string[]
                {
                    "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled",
                    "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber",
                    "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
                },
                values: new object[]
                {
                    "16552ac6-8620-4e04-bbc9-a74ca093d4d0", 0, "e5b737e2-7fa8-41ad-9684-bfdd6d398980",
                    "nobody@localhost", true, false, null, "NOBODY@LOCALHOST", "NOBODY",
                    "AQAAAAEAACcQAAAAENSAlCMfmvzU+0+H6LHgXdjPwN5k1cnnntvSCjVz3/+AkwmEc8t3U86kn2r7CX8Axw==", null, false,
                    "8b5b641f-5231-4726-aac0-ea4dbc62f568", false, "nobody"
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new string[]
                {
                    "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled",
                    "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber",
                    "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName"
                },
                values: new object[]
                {
                    "436d9196-4d65-46ab-9eba-e6c335b3348a", 0, "f8a1f456-9423-476c-9a65-f1dc40b38fbb",
                    "anonymous@localhost", true, false, null, "ANONYMOUS@LOCALHOST", "ANONYMOUS",
                    "AQAAAAEAACcQAAAAEJxOiLkY1rmQmVZx54iv26kLvaspCM8AieXMP03AY3Td+HTTobXHLXYMx894HmbDqw==", null, false,
                    "5ab0058e-9bd9-4861-a06f-f10f7b044107", false, "anonymous"
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "UserName",
                keyValues: new object[] { "root", "nobody", "anonymous" });
        }
    }
}