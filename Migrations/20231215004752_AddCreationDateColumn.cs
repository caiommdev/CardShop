using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardShop.Migrations
{
    public partial class AddCreationDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Card",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Card");
        }
    }
}
