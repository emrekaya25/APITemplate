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
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedIPV4Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedIPV4Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedIPV4Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedIPV4Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedIPV4Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedIPV4Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                columns: new[] { "Id", "AddedIPV4Address", "AddedTime", "Guid", "IsActive", "Name", "UpdatedIPV4Address", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 9, 19, 5, 36, 16, 881, DateTimeKind.Utc).AddTicks(4012), new Guid("aeeb0d53-656e-4b25-9aff-32aa09e6007b"), true, "Admin", null, new DateTime(2024, 9, 19, 5, 36, 16, 881, DateTimeKind.Utc).AddTicks(4016) },
                    { 2, null, new DateTime(2024, 9, 19, 5, 36, 16, 881, DateTimeKind.Utc).AddTicks(4019), new Guid("4f2f59d0-86fd-4589-a44a-7453295abc61"), true, "Çalışan", null, new DateTime(2024, 9, 19, 5, 36, 16, 881, DateTimeKind.Utc).AddTicks(4019) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddedIPV4Address", "AddedTime", "Email", "Guid", "Image", "IsActive", "LastName", "Name", "Password", "UpdatedIPV4Address", "UpdatedTime" },
                values: new object[] { 1, null, new DateTime(2024, 9, 19, 8, 36, 16, 881, DateTimeKind.Local).AddTicks(743), "admin@gmail.com", new Guid("cf1caa7a-aebb-4627-a6e1-d6a1bd7186c2"), "string", true, "Admin", "Admin", "$2a$11$6XbBHPTcxexgLCNgb8yjke7c9s89evPF16XNQoaZxcXgYzvDg3.Ce", null, new DateTime(2024, 9, 19, 8, 36, 16, 881, DateTimeKind.Local).AddTicks(759) });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "AddedIPV4Address", "AddedTime", "Guid", "IsActive", "RoleId", "UpdatedIPV4Address", "UpdatedTime", "UserId" },
                values: new object[] { 1, null, new DateTime(2024, 9, 19, 8, 36, 16, 881, DateTimeKind.Local).AddTicks(9376), new Guid("8f1e5819-370c-4981-9fe3-bb4fb0bb5e69"), true, 1, null, new DateTime(2024, 9, 19, 8, 36, 16, 881, DateTimeKind.Local).AddTicks(9385), 1 });

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
