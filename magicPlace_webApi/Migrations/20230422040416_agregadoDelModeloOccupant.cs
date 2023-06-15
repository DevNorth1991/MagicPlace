using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace magicPlace_webApi.Migrations
{
    public partial class agregadoDelModeloOccupant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occupants",
                columns: table => new
                {
                    IdCard = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    NameOccupant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreationOcccupant = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdateOcccupant = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupants", x => x.IdCard);
                    table.ForeignKey(
                        name: "FK_Occupants_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_RoomId",
                table: "Occupants",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occupants");

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(307), new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(316) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(318), new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(318) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreationDate", "UpdateTime" },
                values: new object[] { new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(320), new DateTime(2023, 4, 17, 19, 36, 0, 81, DateTimeKind.Local).AddTicks(320) });
        }
    }
}
