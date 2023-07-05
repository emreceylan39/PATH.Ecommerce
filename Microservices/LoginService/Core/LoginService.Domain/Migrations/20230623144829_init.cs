using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoginService.Domain.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 23, 17, 48, 29, 175, DateTimeKind.Local).AddTicks(4795), "user1@mail.com", "User", "Bir", "user1" },
                    { 2, new DateTime(2023, 6, 23, 17, 48, 29, 175, DateTimeKind.Local).AddTicks(4806), "user2@mail.com", "User", "Iki", "user2" },
                    { 3, new DateTime(2023, 6, 23, 17, 48, 29, 175, DateTimeKind.Local).AddTicks(4808), "user3@mail.com", "User", "Uc", "user3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
