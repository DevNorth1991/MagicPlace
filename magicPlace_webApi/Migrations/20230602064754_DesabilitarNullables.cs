using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magicPlace_webApi.Migrations
{
    public partial class DesabilitarNullables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9951), new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9961) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9964), new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9964) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9966), new DateTime(2023, 4, 22, 1, 6, 27, 148, DateTimeKind.Local).AddTicks(9966) });
        }
    }
}
