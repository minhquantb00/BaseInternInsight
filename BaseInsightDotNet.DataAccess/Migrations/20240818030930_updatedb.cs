using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractAppendix_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppendixDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractAppendix_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractAppendix_tbl_Contract_tbl_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractHistory_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractHistory_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractHistory_tbl_Contract_tbl_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payroll_tbl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OvertimeSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AllowanceSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductivitySalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OtherIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ToltalSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll_tbl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payroll_tbl_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractAppendix_tbl_ContractId",
                table: "ContractAppendix_tbl",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractHistory_tbl_ContractId",
                table: "ContractHistory_tbl",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_tbl_UserId",
                table: "Payroll_tbl",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractAppendix_tbl");

            migrationBuilder.DropTable(
                name: "ContractHistory_tbl");

            migrationBuilder.DropTable(
                name: "Payroll_tbl");
        }
    }
}
