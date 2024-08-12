using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("0749047a-3311-42d5-a74a-c8efaa3aaf76"));

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("7b0b45ab-9d4c-44b7-bbb5-3dd6cee1ca63"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "af2362f8-3b19-43df-a62e-73eb8f9ba469", "1", "Admin", "Admin" },
                    { "c57224bc-5482-460a-937b-5e2862bbcb4f", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "MediaFolders",
                columns: new[] { "Id", "CanDetectTracks", "Deleted", "FilesCount", "IsPrivate", "IsProtected", "IsPublic", "Metadata", "Name", "Owner", "ParentId", "Slug" },
                values: new object[,]
                {
                    { new Guid("0be2cdd9-9c63-43e0-a37c-1392f901b2b7"), false, false, 0, "", "", true, "", "FilesUpload", "", null, "" },
                    { new Guid("7c28e0cc-1e40-4d79-b8b1-2de8d90a50bb"), true, false, 0, "", "", true, "", "Public", "", null, "" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af2362f8-3b19-43df-a62e-73eb8f9ba469");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c57224bc-5482-460a-937b-5e2862bbcb4f");

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("0be2cdd9-9c63-43e0-a37c-1392f901b2b7"));

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("7c28e0cc-1e40-4d79-b8b1-2de8d90a50bb"));

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b7033375-1c6b-43a8-8d63-a7cecb67a88d", "1", "Admin", "Admin" },
                    { "e706a233-ca3a-47da-b447-292f913fb227", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "MediaFolders",
                columns: new[] { "Id", "CanDetectTracks", "Deleted", "FilesCount", "IsPrivate", "IsProtected", "IsPublic", "Metadata", "Name", "Owner", "ParentId", "Slug" },
                values: new object[,]
                {
                    { new Guid("0749047a-3311-42d5-a74a-c8efaa3aaf76"), false, false, 0, "", "", true, "", "FilesUpload", "", null, "" },
                    { new Guid("7b0b45ab-9d4c-44b7-bbb5-3dd6cee1ca63"), true, false, 0, "", "", true, "", "Public", "", null, "" }
                });
        }
    }
}
