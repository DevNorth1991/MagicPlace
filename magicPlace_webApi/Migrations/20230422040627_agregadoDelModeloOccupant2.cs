using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magicPlace_webApi.Migrations
{
    public partial class agregadoDelModeloOccupant2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 22, 1, 4, 15, 796, DateTimeKind.Local).AddTicks(4345), new DateTime(2023, 4, 22, 1, 4, 15, 796, DateTimeKind.Local).AddTicks(4356) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 22, 1, 4, 15, 796, DateTimeKind.Local).AddTicks(4359), new DateTime(2023, 4, 22, 1, 4, 15, 796, DateTimeKind.Local).AddTicks(4359) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 22, 1, 4, 15, 796, DateTimeKind.Local).AddTicks(4364), new DateTime(2023, 4, 22, 1, 4, 15, 796, DateTimeKind.Local).AddTicks(4365) });
        }
    }
}
