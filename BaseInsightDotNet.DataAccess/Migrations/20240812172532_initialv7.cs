using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class initialv7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmEmail_AspNetUsers_UserId",
                table: "ConfirmEmail");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_AspNetUsers_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfirmEmail",
                table: "ConfirmEmail");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshToken_tbl");

            migrationBuilder.RenameTable(
                name: "ConfirmEmail",
                newName: "ConfirmEmail_tbl");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken_tbl",
                newName: "IX_RefreshToken_tbl_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfirmEmail_UserId",
                table: "ConfirmEmail_tbl",
                newName: "IX_ConfirmEmail_tbl_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken_tbl",
                table: "RefreshToken_tbl",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfirmEmail_tbl",
                table: "ConfirmEmail_tbl",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Allowance_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowanceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowance_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractType_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractType_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slogan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfMember = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_tbl_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Position_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryCoefficient = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contract_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    SalaryBeforeTax = table.Column<double>(type: "float", nullable: false),
                    ContractStatus = table.Column<int>(type: "int", nullable: false),
                    TaxPercentage = table.Column<double>(type: "float", nullable: false),
                    ContractTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SignatureA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSubsidized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contract_tbl_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_tbl_ContractType_tbl_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalTable: "ContractType_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractAllowance_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllowanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAllowance_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAllowance_tbl_Allowance_tbl_AllowanceId",
                        column: x => x.AllowanceId,
                        principalTable: "Allowance_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractAllowance_tbl_Contract_tbl_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_tbl_ContractTypeId",
                table: "Contract_tbl",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_tbl_EmployeeId",
                table: "Contract_tbl",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAllowance_tbl_AllowanceId",
                table: "ContractAllowance_tbl",
                column: "AllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAllowance_tbl_ContractId",
                table: "ContractAllowance_tbl",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_tbl_UserId",
                table: "Notification_tbl",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ConfirmEmail_tbl_AspNetUsers_UserId",
                table: "ConfirmEmail_tbl",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_tbl_AspNetUsers_UserId",
                table: "RefreshToken_tbl",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_tbl_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Position_tbl_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ConfirmEmail_tbl_AspNetUsers_UserId",
                table: "ConfirmEmail_tbl");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_tbl_AspNetUsers_UserId",
                table: "RefreshToken_tbl");

            migrationBuilder.DropTable(
                name: "ContractAllowance_tbl");

            migrationBuilder.DropTable(
                name: "Department_tbl");

            migrationBuilder.DropTable(
                name: "Notification_tbl");

            migrationBuilder.DropTable(
                name: "Position_tbl");

            migrationBuilder.DropTable(
                name: "Allowance_tbl");

            migrationBuilder.DropTable(
                name: "Contract_tbl");

            migrationBuilder.DropTable(
                name: "ContractType_tbl");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PositionId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken_tbl",
                table: "RefreshToken_tbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfirmEmail_tbl",
                table: "ConfirmEmail_tbl");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "RefreshToken_tbl",
                newName: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "ConfirmEmail_tbl",
                newName: "ConfirmEmail");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_tbl_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ConfirmEmail_tbl_UserId",
                table: "ConfirmEmail",
                newName: "IX_ConfirmEmail_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfirmEmail",
                table: "ConfirmEmail",
                column: "Id");

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
    }
}
