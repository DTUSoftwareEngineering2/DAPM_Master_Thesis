using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPM.ResourceRegistryMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d67e82d-6a7e-4a0d-bd0f-58fc665b8e0a"));

            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "accepted",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization", "UserRole", "accepted" },
                values: new object[] { new Guid("42d22ce6-5d8f-4f0a-8d37-8a65081a479a"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("1bb895a2-bee9-4c50-949a-e395c05bd9d0"), 0, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42d22ce6-5d8f-4f0a-8d37-8a65081a479a"));

            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "accepted",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization" },
                values: new object[] { new Guid("4d67e82d-6a7e-4a0d-bd0f-58fc665b8e0a"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("4b5b5523-2215-4c05-8549-274b3bb050d9") });
        }
    }
}
