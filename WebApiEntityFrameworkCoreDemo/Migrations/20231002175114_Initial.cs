using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiEntityFrameworkCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "Name" },
                values: new object[,]
                {
                    { new Guid("cb7ac236-ebc6-4e4b-88f8-863e453aad76"), new DateTime(1910, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petya R" },
                    { new Guid("f1905e08-a903-45e2-973c-1ea1aa7d1eb1"), new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vasya P" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("80ce8f39-4696-4e5a-aa11-7a395d9a45f7"), new Guid("f1905e08-a903-45e2-973c-1ea1aa7d1eb1"), "Description2", 113.11m, "Title2" },
                    { new Guid("870bc805-51e3-41c4-88a8-897bb3108c3b"), new Guid("cb7ac236-ebc6-4e4b-88f8-863e453aad76"), "Description3", 1523.11m, "Title3" },
                    { new Guid("b9f223f4-db20-40e7-bacd-6ca24c6606cd"), new Guid("f1905e08-a903-45e2-973c-1ea1aa7d1eb1"), "Description1", 123.11m, "Title1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
