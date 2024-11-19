using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPM.RepositoryMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangePipelines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "Pipelines",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "visibility",
                table: "Pipelines",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "visibility",
                table: "Pipelines");
        }
    }
}
