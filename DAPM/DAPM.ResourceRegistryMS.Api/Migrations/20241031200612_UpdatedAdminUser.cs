using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPM.ResourceRegistryMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42d22ce6-5d8f-4f0a-8d37-8a65081a479a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization", "UserRole", "accepted" },
                values: new object[] { new Guid("5dbcf6fe-6d6f-4cb0-b7b8-b660eba84487"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("3f2b671c-2b54-4160-ae45-11c03fb0771b"), 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5dbcf6fe-6d6f-4cb0-b7b8-b660eba84487"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization", "UserRole", "accepted" },
                values: new object[] { new Guid("42d22ce6-5d8f-4f0a-8d37-8a65081a479a"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("1bb895a2-bee9-4c50-949a-e395c05bd9d0"), 0, 0 });
        }
    }
}
