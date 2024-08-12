using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialv8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_tbl_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Position_tbl_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("10ed7a62-fc50-4e96-a2ed-9ed86c2a5b41"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("334d1c68-ece1-4a5a-b356-4661299716a8"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("40e30a47-4ac1-4f0a-9b84-5295e9b187ee"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("7606a87e-8332-40a1-aec8-f48d381c5d57"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("c6e05e3c-27cc-4067-8d04-8674fb43da67"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("dbefcdb5-3dc8-4f48-8a1b-56962052faff"));

            migrationBuilder.DeleteData(
                table: "Department_tbl",
                keyColumn: "Id",
                keyValue: new Guid("74d87357-18b3-44cd-ab1e-9262cb587f14"));

            migrationBuilder.DeleteData(
                table: "Position_tbl",
                keyColumn: "Id",
                keyValue: new Guid("339a5077-a38e-417b-9370-973f5d967f37"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Allowance_tbl",
                columns: new[] { "Id", "AllowanceName", "Amount" },
                values: new object[,]
                {
                    { new Guid("1ad800bd-2c5d-4e28-a30c-13b8853b2032"), "Phụ cấp ăn trưa", 50.0 },
                    { new Guid("57b38617-b24b-4a93-ba00-f05fa4f5a3e9"), "Phụ cấp đi lại", 100.0 },
                    { new Guid("d34f6c9c-3cf6-4f37-88af-4ff293400016"), "Phụ cấp ăn tối", 60.0 }
                });

            migrationBuilder.InsertData(
                table: "ContractType_tbl",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("22e60153-6b78-463b-bae5-82a20fe71ad5"), "Hợp đồng thử việc", "Thử việc" },
                    { new Guid("7ced46ed-e1a2-425b-a437-b04d19b22254"), "Hợp đồng cộng tác viên", "CTV" },
                    { new Guid("e50e8cf5-5d9c-4f6c-b27b-e2676fc63f40"), "Hợp đồng chính thức", "Chính thức" }
                });

            migrationBuilder.InsertData(
                table: "Department_tbl",
                columns: new[] { "Id", "CreateTime", "ManagerId", "Name", "NumberOfMember", "Slogan", "UpdateTime" },
                values: new object[] { new Guid("97dbd062-9de9-4ede-a3c1-7b80c4c3e253"), new DateTime(2024, 8, 13, 0, 35, 29, 606, DateTimeKind.Local).AddTicks(2511), "1240b4b9-798c-4b54-8b69-e68565dc6ba9", "Dev", 0, "Hế lô", null });

            migrationBuilder.InsertData(
                table: "Position_tbl",
                columns: new[] { "Id", "Name", "SalaryCoefficient" },
                values: new object[] { new Guid("3f7fb70b-5dac-4e5f-bc52-6145d51233dd"), "Thuế thu nhập", 0.1m });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Department_tbl_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Department_tbl",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Position_tbl_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "Position_tbl",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_tbl_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Position_tbl_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("1ad800bd-2c5d-4e28-a30c-13b8853b2032"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("57b38617-b24b-4a93-ba00-f05fa4f5a3e9"));

            migrationBuilder.DeleteData(
                table: "Allowance_tbl",
                keyColumn: "Id",
                keyValue: new Guid("d34f6c9c-3cf6-4f37-88af-4ff293400016"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("22e60153-6b78-463b-bae5-82a20fe71ad5"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("7ced46ed-e1a2-425b-a437-b04d19b22254"));

            migrationBuilder.DeleteData(
                table: "ContractType_tbl",
                keyColumn: "Id",
                keyValue: new Guid("e50e8cf5-5d9c-4f6c-b27b-e2676fc63f40"));

            migrationBuilder.DeleteData(
                table: "Department_tbl",
                keyColumn: "Id",
                keyValue: new Guid("97dbd062-9de9-4ede-a3c1-7b80c4c3e253"));

            migrationBuilder.DeleteData(
                table: "Position_tbl",
                keyColumn: "Id",
                keyValue: new Guid("3f7fb70b-5dac-4e5f-bc52-6145d51233dd"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PositionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Allowance_tbl",
                columns: new[] { "Id", "AllowanceName", "Amount" },
                values: new object[,]
                {
                    { new Guid("10ed7a62-fc50-4e96-a2ed-9ed86c2a5b41"), "Phụ cấp đi lại", 100.0 },
                    { new Guid("334d1c68-ece1-4a5a-b356-4661299716a8"), "Phụ cấp ăn trưa", 50.0 },
                    { new Guid("40e30a47-4ac1-4f0a-9b84-5295e9b187ee"), "Phụ cấp ăn tối", 60.0 }
                });

            migrationBuilder.InsertData(
                table: "ContractType_tbl",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("7606a87e-8332-40a1-aec8-f48d381c5d57"), "Hợp đồng cộng tác viên", "CTV" },
                    { new Guid("c6e05e3c-27cc-4067-8d04-8674fb43da67"), "Hợp đồng chính thức", "Chính thức" },
                    { new Guid("dbefcdb5-3dc8-4f48-8a1b-56962052faff"), "Hợp đồng thử việc", "Thử việc" }
                });

            migrationBuilder.InsertData(
                table: "Department_tbl",
                columns: new[] { "Id", "CreateTime", "ManagerId", "Name", "NumberOfMember", "Slogan", "UpdateTime" },
                values: new object[] { new Guid("74d87357-18b3-44cd-ab1e-9262cb587f14"), new DateTime(2024, 8, 13, 0, 25, 32, 297, DateTimeKind.Local).AddTicks(4285), "1240b4b9-798c-4b54-8b69-e68565dc6ba9", "Dev", 0, "Hế lô", null });

            migrationBuilder.InsertData(
                table: "Position_tbl",
                columns: new[] { "Id", "Name", "SalaryCoefficient" },
                values: new object[] { new Guid("339a5077-a38e-417b-9370-973f5d967f37"), "Thuế thu nhập", 0.1m });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Department_tbl_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Department_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Position_tbl_PositionId",
                table: "AspNetUsers",
                column: "PositionId",
                principalTable: "Position_tbl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
