using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameManagement.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class initalcreate : Migration
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
                    { new Guid("621b65a6-6b42-4a1e-b8f6-5866ed523c9c"), "mw2021", "Free", new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(6634) },
                    { new Guid("e5903e63-ce9a-4e42-886a-f4cbf7cff470"), "dc2021", "Free", new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(6645) }
                });

            migrationBuilder.InsertData(
                table: "Characters",
                columns: new[] { "Id", "DateCreated", "Level", "Nickname", "PlayerId", "classes" },
                values: new object[,]
                {
                    { new Guid("8b43ebef-15d7-4f1f-b967-ed665e5eb8e9"), new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(7640), 29, "asaka", new Guid("621b65a6-6b42-4a1e-b8f6-5866ed523c9c"), "Druid" },
                    { new Guid("b0464319-1102-4a6b-b25a-600e26d391e3"), new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(7502), 99, "Code Man", new Guid("621b65a6-6b42-4a1e-b8f6-5866ed523c9c"), "Mage" },
                    { new Guid("b52284ab-8e59-4d1a-b8e8-cc65af9e20c9"), new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(7641), 5, "MyWon", new Guid("e5903e63-ce9a-4e42-886a-f4cbf7cff470"), "Mage" },
                    { new Guid("c37e010e-ad34-40e2-8150-95436b1647be"), new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(7631), 99, "WZ", new Guid("621b65a6-6b42-4a1e-b8f6-5866ed523c9c"), "Warrior" },
                    { new Guid("e07576f4-5ece-418e-a1e0-a401812f5eaa"), new DateTime(2024, 5, 30, 8, 52, 12, 254, DateTimeKind.Local).AddTicks(7644), 95, "TBD", new Guid("e5903e63-ce9a-4e42-886a-f4cbf7cff470"), "Wizzard" }
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
