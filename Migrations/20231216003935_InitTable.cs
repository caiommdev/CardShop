using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardShop.Migrations
{
    public partial class InitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BoardGame",
                columns: new[] { "Id", "CreationDate", "Game", "ImageFile", "ImageFileName", "Name", "Price" },
                values: new object[] { 1, new DateTime(2023, 12, 15, 21, 39, 35, 543, DateTimeKind.Local).AddTicks(8451), "jogo", null, null, "Jogo do pao", 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BoardGame",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
