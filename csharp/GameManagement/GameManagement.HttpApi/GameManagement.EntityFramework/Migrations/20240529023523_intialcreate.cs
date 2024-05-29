using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameManagement.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class intialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    classes = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Account", "AccountType", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"), "mw2021", "Free", new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(4941) },
                    { new Guid("f02a7e5a-0ea1-4c86-8106-cb6de77c4f22"), "dc2021", "Free", new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(4959) }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "DateCreated", "Level", "Nickname", "PlayerId", "classes" },
                values: new object[,]
                {
                    { new Guid("22302531-0a2c-45ba-9e3f-030423d8ac8e"), new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5971), 5, "MyWon", new Guid("f02a7e5a-0ea1-4c86-8106-cb6de77c4f22"), "Mage" },
                    { new Guid("4c0fad31-4c9a-4880-8f1e-c31f1c4ddc34"), new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5839), 99, "Code Man", new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"), "Mage" },
                    { new Guid("62ce51c8-5baa-4523-9396-25ad886593db"), new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5968), 99, "WZ", new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"), "Warrior" },
                    { new Guid("d7287182-6ce6-42ba-bf53-96d9d32f5304"), new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5979), 95, "TBD", new Guid("f02a7e5a-0ea1-4c86-8106-cb6de77c4f22"), "Wizzard" },
                    { new Guid("e7eed720-bc05-4fc9-9ded-cef58deb97ac"), new DateTime(2024, 5, 29, 10, 35, 21, 985, DateTimeKind.Local).AddTicks(5969), 29, "asaka", new Guid("6ab0d026-b026-4ef9-8727-2c19ba5c54d8"), "Druid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_Nickname",
                table: "Characters",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PlayerId",
                table: "Characters",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Account",
                table: "Players",
                column: "Account",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
