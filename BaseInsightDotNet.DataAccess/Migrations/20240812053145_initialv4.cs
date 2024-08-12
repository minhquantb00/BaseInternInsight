using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialv4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b51e34c-87ea-4264-876a-ae821892d3a4", "2", "User", "User" },
                    { "31ec98e8-0b7b-46a6-841f-9ba606792548", "1", "Admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "MediaFolders",
                columns: new[] { "Id", "CanDetectTracks", "Deleted", "FilesCount", "IsPrivate", "IsProtected", "IsPublic", "Metadata", "Name", "Owner", "ParentId", "Slug" },
                values: new object[,]
                {
                    { new Guid("8e10bcd8-0af0-4667-89fe-5a4839b12a28"), false, false, 0, "", "", true, "", "FilesUpload", "", null, "" },
                    { new Guid("d9fcd73e-e8c8-4737-9165-b18f1eeacdab"), true, false, 0, "", "", true, "", "Public", "", null, "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b51e34c-87ea-4264-876a-ae821892d3a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31ec98e8-0b7b-46a6-841f-9ba606792548");

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("8e10bcd8-0af0-4667-89fe-5a4839b12a28"));

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("d9fcd73e-e8c8-4737-9165-b18f1eeacdab"));
        }
    }
}
