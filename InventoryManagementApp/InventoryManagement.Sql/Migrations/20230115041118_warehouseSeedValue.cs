using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Sql.Migrations
{
    public partial class warehouseSeedValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Flag",
                value: "gb");

            migrationBuilder.InsertData(
                table: "WareHouses",
                columns: new[] { "Id", "CompanyId", "CountryId", "CreatedBy", "CreatedDateUtc", "Location", "Name", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[] { 1L, 1L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Dhanmondi", "RedX", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WareHouses",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Flag",
                value: "uk");
        }
    }
}
