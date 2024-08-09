using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialver40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.CreateTable(
                name: "MediaFolders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanDetectTracks = table.Column<bool>(type: "bit", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilesCount = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    IsProtected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFolders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFolders_MediaFolders_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MediaFolders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MediaStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaStorages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FolderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    PixelSize = table.Column<int>(type: "int", nullable: true),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    IsTransient = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Hidden = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    MediaStorageId = table.Column<int>(type: "int", nullable: true),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaStorageId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaFiles_MediaFolders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "MediaFolders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MediaFiles_MediaStorages_MediaStorageId1",
                        column: x => x.MediaStorageId1,
                        principalTable: "MediaStorages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Downloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DownloadGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UseDownloadUrl = table.Column<bool>(type: "bit", nullable: false),
                    DownloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTransient = table.Column<bool>(type: "bit", nullable: false),
                    MediaFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Changelog = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Downloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Downloads_MediaFiles_MediaFileId",
                        column: x => x.MediaFileId,
                        principalTable: "MediaFiles",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MediaFolders",
                columns: new[] { "Id", "CanDetectTracks", "Deleted", "FilesCount", "IsPrivate", "IsProtected", "IsPublic", "Metadata", "Name", "Owner", "ParentId", "Slug" },
                values: new object[] { new Guid("48d5d2f9-6f02-42d6-b64c-4efb794da53f"), false, false, 0, "", "", true, "", "FilesUpload", "", null, "" });

            migrationBuilder.InsertData(
                table: "MediaFolders",
                columns: new[] { "Id", "CanDetectTracks", "Deleted", "FilesCount", "IsPrivate", "IsProtected", "IsPublic", "Metadata", "Name", "Owner", "ParentId", "Slug" },
                values: new object[] { new Guid("e3b055d4-4739-4a50-b039-dcc31a81e13e"), true, false, 0, "", "", true, "", "Public", "", null, "" });

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_MediaFileId",
                table: "Downloads",
                column: "MediaFileId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_FolderId",
                table: "MediaFiles",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_MediaStorageId1",
                table: "MediaFiles",
                column: "MediaStorageId1");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFolders_ParentId",
                table: "MediaFolders",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Downloads");

            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.DropTable(
                name: "MediaFolders");

            migrationBuilder.DropTable(
                name: "MediaStorages");

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
                values: new object[] { "2a0d772d-fbdd-4497-bea8-e988dc31d57b", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dce1f377-73c5-41e8-97ce-e08439cc377e", "1", "Admin", "Admin" });
        }
    }
}
