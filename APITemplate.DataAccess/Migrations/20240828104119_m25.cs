using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APITemplate.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class m25 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "AddedTime", "IsActive", "Name", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(693), true, "Admin", new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(694) },
                    { 2, new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(696), true, "Çalışan", new DateTime(2024, 8, 28, 10, 41, 19, 269, DateTimeKind.Utc).AddTicks(697) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddedTime", "Email", "Image", "IsActive", "LastName", "Name", "Password", "UpdatedTime" },
                values: new object[] { 1, new DateTime(2024, 8, 28, 13, 41, 19, 268, DateTimeKind.Local).AddTicks(9747), "admin@gmail.com", "string", true, "Admin", "Admin", "123", new DateTime(2024, 8, 28, 13, 41, 19, 268, DateTimeKind.Local).AddTicks(9760) });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "AddedTime", "IsActive", "RoleId", "UpdatedTime", "UserId" },
                values: new object[] { 1, new DateTime(2024, 8, 28, 13, 41, 19, 269, DateTimeKind.Local).AddTicks(3344), true, 1, new DateTime(2024, 8, 28, 13, 41, 19, 269, DateTimeKind.Local).AddTicks(3348), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
