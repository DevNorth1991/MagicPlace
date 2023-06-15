using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magicPlace_webApi.Migrations
{
    public partial class alimentarTabla : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CreationDate", "Detail", "Fee", "ImageUrl", "Name", "Occupants", "SquareMeters", "UpdateTime" },
                values: new object[] { 1, new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(307), "", 125.0, "", "habitacion magica", 4, 16, new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(316) });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CreationDate", "Detail", "Fee", "ImageUrl", "Name", "Occupants", "SquareMeters", "UpdateTime" },
                values: new object[] { 2, new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(318), "", 1109.0, "", "habitacion excellent", 4, 16, new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(318) });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "CreationDate", "Detail", "Fee", "ImageUrl", "Name", "Occupants", "SquareMeters", "UpdateTime" },
                values: new object[] { 3, new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(320), "", 100.0, "", "habitacion premium ", 4, 16, new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(320) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
