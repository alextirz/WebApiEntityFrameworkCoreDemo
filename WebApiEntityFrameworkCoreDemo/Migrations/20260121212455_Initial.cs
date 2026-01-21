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
                    { new Guid("693bf6f7-71f2-4706-a6cb-5511c978f511"), new DateTime(1910, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petya R" },
                    { new Guid("c374e5f1-cc7c-4248-aad8-8f87bd5aa3e6"), new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vasya P" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("10e9643b-4691-4cb3-b00c-09895ee81acc"), new Guid("c374e5f1-cc7c-4248-aad8-8f87bd5aa3e6"), "Description2", 113.11m, "Title2" },
                    { new Guid("639a71e2-d519-46c8-896d-d81ccbfefc14"), new Guid("c374e5f1-cc7c-4248-aad8-8f87bd5aa3e6"), "Description1", 123.11m, "Title1" },
                    { new Guid("cce169b1-065f-40e2-8a74-403042d52ef0"), new Guid("693bf6f7-71f2-4706-a6cb-5511c978f511"), "Description3", 1523.11m, "Title3" }
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
