using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPM.ResourceRegistryMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization" },
                values: new object[] { new Guid("4d67e82d-6a7e-4a0d-bd0f-58fc665b8e0a"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("4b5b5523-2215-4c05-8549-274b3bb050d9") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d67e82d-6a7e-4a0d-bd0f-58fc665b8e0a"));
        }
    }
}
