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
                    { 1, null, new DateTime(2024, 9, 18, 5, 42, 5, 761, DateTimeKind.Utc).AddTicks(383), new Guid("ffdf1936-5e41-49c8-976f-06568cbf37de"), true, "Admin", null, new DateTime(2024, 9, 18, 5, 42, 5, 761, DateTimeKind.Utc).AddTicks(384) },
                    { 2, null, new DateTime(2024, 9, 18, 5, 42, 5, 761, DateTimeKind.Utc).AddTicks(387), new Guid("5910b8a6-e4f2-4819-a71b-b6494e514ffa"), true, "Çalışan", null, new DateTime(2024, 9, 18, 5, 42, 5, 761, DateTimeKind.Utc).AddTicks(388) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddedIPV4Address", "AddedTime", "Email", "Guid", "Image", "IsActive", "LastName", "Name", "Password", "UpdatedIPV4Address", "UpdatedTime" },
                values: new object[] { 1, null, new DateTime(2024, 9, 18, 8, 42, 5, 760, DateTimeKind.Local).AddTicks(9067), "admin@gmail.com", new Guid("78bda474-0196-40e9-849c-2ad2ddec3ff8"), "string", true, "Admin", "Admin", "123", null, new DateTime(2024, 9, 18, 8, 42, 5, 760, DateTimeKind.Local).AddTicks(9080) });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "AddedIPV4Address", "AddedTime", "Guid", "IsActive", "RoleId", "UpdatedIPV4Address", "UpdatedTime", "UserId" },
                values: new object[] { 1, null, new DateTime(2024, 9, 18, 8, 42, 5, 761, DateTimeKind.Local).AddTicks(3877), new Guid("19c84e60-1c6f-4a7f-bc9b-cfe20fcdc0b8"), true, 1, null, new DateTime(2024, 9, 18, 8, 42, 5, 761, DateTimeKind.Local).AddTicks(3881), 1 });

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
