using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAPM.RepositoryMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class ListExecDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<DateTime>>(
                name: "ExecutionDate",
                table: "Pipelines",
                type: "timestamp with time zone[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExecutionDate",
                table: "Pipelines");
        }
    }
}
