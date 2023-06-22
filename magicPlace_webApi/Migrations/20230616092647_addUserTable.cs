using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magicPlace_webApi.Migrations
{
    public partial class addUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 6, 16, 6, 26, 46, 671, DateTimeKind.Local).AddTicks(3483), new DateTime(2023, 6, 16, 6, 26, 46, 671, DateTimeKind.Local).AddTicks(3630) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 6, 16, 6, 26, 46, 671, DateTimeKind.Local).AddTicks(3638), new DateTime(2023, 6, 16, 6, 26, 46, 671, DateTimeKind.Local).AddTicks(3638) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 6, 16, 6, 26, 46, 671, DateTimeKind.Local).AddTicks(3640), new DateTime(2023, 6, 16, 6, 26, 46, 671, DateTimeKind.Local).AddTicks(3640) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 6, 2, 3, 47, 54, 415, DateTimeKind.Local).AddTicks(3118), new DateTime(2023, 6, 2, 3, 47, 54, 415, DateTimeKind.Local).AddTicks(3127) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 6, 2, 3, 47, 54, 415, DateTimeKind.Local).AddTicks(3129), new DateTime(2023, 6, 2, 3, 47, 54, 415, DateTimeKind.Local).AddTicks(3130) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 6, 2, 3, 47, 54, 415, DateTimeKind.Local).AddTicks(3131), new DateTime(2023, 6, 2, 3, 47, 54, 415, DateTimeKind.Local).AddTicks(3132) });
        }
    }
}
