using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CrowdfindingApp.Data.Migrations
{
    public partial class DefaultRolesCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Permissions" },
                values: new object[] { new Guid("bcb8a6f1-275b-4972-b7b7-11a892c5c28f"), "DefaultUser", "" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Permissions" },
                values: new object[] { new Guid("438fd108-d9b1-401e-8d5b-27002b383a27"), "Admin", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("438fd108-d9b1-401e-8d5b-27002b383a27"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bcb8a6f1-275b-4972-b7b7-11a892c5c28f"));
        }
    }
}
