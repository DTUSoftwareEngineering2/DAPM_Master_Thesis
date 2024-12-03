using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPM.ResourceRegistryMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class DefaultAdminAccpeted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5dbcf6fe-6d6f-4cb0-b7b8-b660eba84487"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization", "UserRole", "accepted" },
                values: new object[] { new Guid("b336b79e-4b33-4ab3-a1c3-d0687b6cdb0f"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("d5605255-3474-4ba4-a4bd-8229d835b27e"), 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b336b79e-4b33-4ab3-a1c3-d0687b6cdb0f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "HashPassword", "LastName", "Mail", "Organization", "UserRole", "accepted" },
                values: new object[] { new Guid("5dbcf6fe-6d6f-4cb0-b7b8-b660eba84487"), "admin", "$2a$12$Jligef.ByeRACdblRiMuDejgNYXlUBZWfCSD3wTZ029g5MF/x8cDa", "admin", "admin@email.ch", new Guid("3f2b671c-2b54-4160-ae45-11c03fb0771b"), 1, 1 });
        }
    }
}
