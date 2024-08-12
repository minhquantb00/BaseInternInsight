using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmEmail_AspNetUsers_UserId1",
                table: "ConfirmEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId1",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserId1",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmEmail_UserId1",
                table: "ConfirmEmail");

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

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ConfirmEmail");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ConfirmEmail",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmEmail_UserId",
                table: "ConfirmEmail",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmEmail_AspNetUsers_UserId",
                table: "ConfirmEmail",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmEmail_AspNetUsers_UserId",
                table: "ConfirmEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_ConfirmEmail_UserId",
                table: "ConfirmEmail");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "RefreshToken",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ConfirmEmail",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ConfirmEmail",
                type: "nvarchar(450)",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId1",
                table: "RefreshToken",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmEmail_UserId1",
                table: "ConfirmEmail",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmEmail_AspNetUsers_UserId1",
                table: "ConfirmEmail",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId1",
                table: "RefreshToken",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
