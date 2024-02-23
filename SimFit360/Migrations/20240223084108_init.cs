using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SimFit360.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Maschines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maschines", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserBadges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BadgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Pincode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Kcal = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MaschineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Maschines_MaschineId",
                        column: x => x.MaschineId,
                        principalTable: "Maschines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Activities_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Beginner" },
                    { 2, "Intermediate" },
                    { 3, "Advanced" },
                    { 4, "Expert" }
                });

            migrationBuilder.InsertData(
                table: "Maschines",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Treadmill" },
                    { 2, "Bike" },
                    { 3, "Rowing machine" },
                    { 4, "Cross trainer" }
                });

            migrationBuilder.InsertData(
                table: "UserBadges",
                columns: new[] { "Id", "BadgeId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 3 },
                    { 4, 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Pincode" },
                values: new object[,]
                {
                    { 1, "jeroen@gmail.com", "Jeroen", 0 },
                    { 2, "boyan@gmail.com", "Boyan", 1111 },
                    { 3, "caltan@gmail.com", "Caltan", 2222 },
                    { 4, "junbo@gmail.com", "Junbo", 3333 }
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Date", "Kcal", "MaschineId", "Time", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, 1, new TimeSpan(0, 0, 35, 0, 0), 1 },
                    { 2, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 300, 2, new TimeSpan(0, 0, 40, 0, 0), 2 },
                    { 3, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 400, 3, new TimeSpan(0, 0, 50, 0, 0), 3 },
                    { 4, new DateTime(2024, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 500, 4, new TimeSpan(0, 1, 5, 0, 0), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_MaschineId",
                table: "Activities",
                column: "MaschineId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_UserId",
                table: "Activities",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "UserBadges");

            migrationBuilder.DropTable(
                name: "Maschines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
