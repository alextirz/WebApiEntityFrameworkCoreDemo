using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiEntityFrameworkCoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("10e9643b-4691-4cb3-b00c-09895ee81acc"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("639a71e2-d519-46c8-896d-d81ccbfefc14"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("cce169b1-065f-40e2-8a74-403042d52ef0"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("693bf6f7-71f2-4706-a6cb-5511c978f511"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("c374e5f1-cc7c-4248-aad8-8f87bd5aa3e6"));

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "BirthDate", "Name" },
                values: new object[,]
                {
                    { new Guid("faf8ed2a-e995-4933-ad0b-4f34366b9a1f"), new DateTime(1910, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Petya R" },
                    { new Guid("fdf77ea8-6758-482d-93f4-83e0da2517fb"), new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vasya P" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("2c5d2764-8c5f-4755-b11f-10e457ded0cf"), "Description1", 123.11m, "Title1" },
                    { new Guid("78606e63-e180-4d09-b668-3744a26457c0"), "Description3", 1523.11m, "Title3" },
                    { new Guid("f54d8cd9-eb63-44d9-8fa7-64e397b8c77b"), "Description2", 113.11m, "Title2" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { new Guid("faf8ed2a-e995-4933-ad0b-4f34366b9a1f"), new Guid("2c5d2764-8c5f-4755-b11f-10e457ded0cf") },
                    { new Guid("faf8ed2a-e995-4933-ad0b-4f34366b9a1f"), new Guid("78606e63-e180-4d09-b668-3744a26457c0") },
                    { new Guid("fdf77ea8-6758-482d-93f4-83e0da2517fb"), new Guid("2c5d2764-8c5f-4755-b11f-10e457ded0cf") },
                    { new Guid("fdf77ea8-6758-482d-93f4-83e0da2517fb"), new Guid("f54d8cd9-eb63-44d9-8fa7-64e397b8c77b") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("faf8ed2a-e995-4933-ad0b-4f34366b9a1f"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("fdf77ea8-6758-482d-93f4-83e0da2517fb"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2c5d2764-8c5f-4755-b11f-10e457ded0cf"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("78606e63-e180-4d09-b668-3744a26457c0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("f54d8cd9-eb63-44d9-8fa7-64e397b8c77b"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
