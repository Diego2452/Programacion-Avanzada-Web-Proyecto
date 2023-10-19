using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoProgramaciónAvanzadaWeb.Migrations
{
    public partial class RolesSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "495283f6-ef46-4397-a1a5-05d6b17d9803", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7f6a3f0-2f18-4c55-96d7-20fa421be4db", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "495283f6-ef46-4397-a1a5-05d6b17d9803");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7f6a3f0-2f18-4c55-96d7-20fa421be4db");
        }
    }
}
