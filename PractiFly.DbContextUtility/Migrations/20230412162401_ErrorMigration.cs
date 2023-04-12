using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PractiFly.DbContextUtility.Migrations
{
    /// <inheritdoc />
    public partial class ErrorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ErrorType = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    StackTrace = table.Column<string>(type: "text", nullable: false),
                    ExceptionName = table.Column<string>(type: "text", nullable: false),
                    GeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorEntities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ErrorEntities",
                columns: new[] { "Id", "ErrorType", "ExceptionName", "GeneratedAt", "Message", "StackTrace" },
                values: new object[] { 1, 0, "Unknown error", new DateTime(2023, 4, 12, 19, 24, 0, 930, DateTimeKind.Local).AddTicks(5317), "Unknown error", "Unknown error" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorEntities");
        }
    }
}
