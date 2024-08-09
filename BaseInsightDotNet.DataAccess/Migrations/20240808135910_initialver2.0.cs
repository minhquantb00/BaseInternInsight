using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialver20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "474fe077-680b-4600-bd8a-4c9d66e21d5d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "92a72792-a5e9-4458-950f-f2e4f7213d69");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2a0d772d-fbdd-4497-bea8-e988dc31d57b", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dce1f377-73c5-41e8-97ce-e08439cc377e", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "2a0d772d-fbdd-4497-bea8-e988dc31d57b");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "dce1f377-73c5-41e8-97ce-e08439cc377e");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "474fe077-680b-4600-bd8a-4c9d66e21d5d", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "92a72792-a5e9-4458-950f-f2e4f7213d69", "1", "Admin", "Admin" });
        }
    }
}
