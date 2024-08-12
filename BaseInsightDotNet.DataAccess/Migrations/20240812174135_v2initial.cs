using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class v2initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("2279741c-d144-4b82-b9fd-919621bc64f0"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("2aa71727-af41-4435-a6b6-cfee77a44ddd"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("8429963c-1e95-42b3-a366-7b17219c3eaa"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("09baf487-b531-400f-99da-2ed9f57dce7f"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("5d088140-8ea2-4557-8b69-3661d5ce0a41"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("8aa2b6a9-a626-4e8d-bb80-2c637400a2cc"));

            migrationBuilder.DeleteData(
                table: "Department_tbl",
                keyColumn: "Id",
                keyValue: new Guid("3893968c-5665-4d02-b0e4-b7f40f2011b5"));

            migrationBuilder.DeleteData(
                table: "Position_tbl",
                keyColumn: "Id",
                keyValue: new Guid("2ba5534c-bd17-4e0c-8dbe-98de4d4fde08"));

            migrationBuilder.InsertData(
                table: "Allowance_tbl",
                columns: new[] { "Id", "AllowanceName", "Amount" },
                values: new object[,]
                {
                    { new Guid("7664fd57-8ac7-46d8-a210-1139c6fc38e4"), "Phụ cấp ăn tối", 60.0 },
                    { new Guid("ca730861-1dfa-4c39-af50-8a2fe3bb21cd"), "Phụ cấp đi lại", 100.0 },
                    { new Guid("f889bc85-f3d5-4dbd-b8f1-46508f608120"), "Phụ cấp ăn trưa", 50.0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cb2b0c7-ebd0-42f8-8022-1e2f72585339", "1", "Admin", "Admin" },
                    { "769748e0-3525-4574-82ff-fb8498a2f509", "2", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "ContractType_tbl",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("47a61fd8-d77c-4352-920f-fa140b2761d9"), "Hợp đồng chính thức", "Chính thức" },
                    { new Guid("be31333d-5bdb-4831-85e7-cd19335fad2c"), "Hợp đồng thử việc", "Thử việc" },
                    { new Guid("f5c32e66-20b1-42e3-9ee7-195884909b82"), "Hợp đồng cộng tác viên", "CTV" }
                });

            migrationBuilder.InsertData(
                table: "Department_tbl",
                columns: new[] { "Id", "CreateTime", "ManagerId", "Name", "NumberOfMember", "Slogan", "UpdateTime" },
                values: new object[] { new Guid("c60d73c3-b134-4239-9f03-2744cef6578d"), new DateTime(2024, 8, 13, 0, 41, 34, 972, DateTimeKind.Local).AddTicks(4031), "1240b4b9-798c-4b54-8b69-e68565dc6ba9", "Dev", 0, "Hế lô", null });

            migrationBuilder.InsertData(
                table: "MediaFolders",
                columns: new[] { "Id", "CanDetectTracks", "Deleted", "FilesCount", "IsPrivate", "IsProtected", "IsPublic", "Metadata", "Name", "Owner", "ParentId", "Slug" },
                values: new object[,]
                {
                    { new Guid("df26a618-1929-4c60-97d6-32077bf6e3d1"), false, false, 0, "", "", true, "", "FilesUpload", "", null, "" },
                    { new Guid("f853aa06-daea-469a-a196-8328d26067a9"), true, false, 0, "", "", true, "", "Public", "", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Position_tbl",
                columns: new[] { "Id", "Name", "SalaryCoefficient" },
                values: new object[] { new Guid("e91511a4-193e-41a6-8738-9021c2f0e744"), "Thuế thu nhập", 0.1m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("7664fd57-8ac7-46d8-a210-1139c6fc38e4"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("ca730861-1dfa-4c39-af50-8a2fe3bb21cd"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("f889bc85-f3d5-4dbd-b8f1-46508f608120"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cb2b0c7-ebd0-42f8-8022-1e2f72585339");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "769748e0-3525-4574-82ff-fb8498a2f509");

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("47a61fd8-d77c-4352-920f-fa140b2761d9"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("be31333d-5bdb-4831-85e7-cd19335fad2c"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("f5c32e66-20b1-42e3-9ee7-195884909b82"));

            migrationBuilder.DeleteData(
                table: "Department_tbl",
                keyColumn: "Id",
                keyValue: new Guid("c60d73c3-b134-4239-9f03-2744cef6578d"));

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("df26a618-1929-4c60-97d6-32077bf6e3d1"));

            migrationBuilder.DeleteData(
                table: "MediaFolders",
                keyColumn: "Id",
                keyValue: new Guid("f853aa06-daea-469a-a196-8328d26067a9"));

            migrationBuilder.DeleteData(
                table: "Position_tbl",
                keyColumn: "Id",
                keyValue: new Guid("e91511a4-193e-41a6-8738-9021c2f0e744"));

            migrationBuilder.InsertData(
                table: "Allowance_tbl",
                columns: new[] { "Id", "AllowanceName", "Amount" },
                values: new object[,]
                {
                    { new Guid("2279741c-d144-4b82-b9fd-919621bc64f0"), "Phụ cấp ăn trưa", 50.0 },
                    { new Guid("2aa71727-af41-4435-a6b6-cfee77a44ddd"), "Phụ cấp ăn tối", 60.0 },
                    { new Guid("8429963c-1e95-42b3-a366-7b17219c3eaa"), "Phụ cấp đi lại", 100.0 }
                });

            migrationBuilder.InsertData(
                table: "ContractType_tbl",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("09baf487-b531-400f-99da-2ed9f57dce7f"), "Hợp đồng cộng tác viên", "CTV" },
                    { new Guid("5d088140-8ea2-4557-8b69-3661d5ce0a41"), "Hợp đồng thử việc", "Thử việc" },
                    { new Guid("8aa2b6a9-a626-4e8d-bb80-2c637400a2cc"), "Hợp đồng chính thức", "Chính thức" }
                });

            migrationBuilder.InsertData(
                table: "Department_tbl",
                columns: new[] { "Id", "CreateTime", "ManagerId", "Name", "NumberOfMember", "Slogan", "UpdateTime" },
                values: new object[] { new Guid("3893968c-5665-4d02-b0e4-b7f40f2011b5"), new DateTime(2024, 8, 13, 0, 40, 59, 835, DateTimeKind.Local).AddTicks(2565), "1240b4b9-798c-4b54-8b69-e68565dc6ba9", "Dev", 0, "Hế lô", null });

            migrationBuilder.InsertData(
                table: "Position_tbl",
                columns: new[] { "Id", "Name", "SalaryCoefficient" },
                values: new object[] { new Guid("2ba5534c-bd17-4e0c-8dbe-98de4d4fde08"), "Thuế thu nhập", 0.1m });
        }
    }
}
