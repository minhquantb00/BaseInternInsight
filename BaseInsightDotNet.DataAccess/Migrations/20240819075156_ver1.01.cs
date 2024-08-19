using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseInsightDotNet.DataAccess.Migrations
{
    public partial class ver101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveDate",
                table: "ContractAllowance_tbl",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "ContractAllowance_tbl",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceiveAllowance",
                table: "Contract_tbl",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveDate",
                table: "ContractAllowance_tbl");

            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "ContractAllowance_tbl");

            migrationBuilder.DropColumn(
                name: "ReceiveAllowance",
                table: "Contract_tbl");

            migrationBuilder.DropColumn(
                name: "Account",
                table: "AspNetUsers");
        }
    }
}
