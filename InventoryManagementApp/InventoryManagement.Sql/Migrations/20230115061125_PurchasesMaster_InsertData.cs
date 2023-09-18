using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Sql.Migrations
{
    public partial class PurchasesMaster_InsertData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PurchasesMasters",
                columns: new[] { "Id", "Attn", "Cancle", "CompanyId", "CreatedBy", "CreatedDateUtc", "DiscountAmount", "DiscountPercent", "LcDate", "LcNumber", "PaymentType", "PoNumber", "PurchasesCode", "PurchasesDate", "PurchasesType", "Remarks", "SupplierId", "UpdatedBy", "UpdatedDateUtc", "UserId", "VatAmount", "VatPercent", "Warrenty" },
                values: new object[] { 1L, "None", false, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), 50f, 12f, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Local), 15603f, "Cash", 145230f, "AD1045", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Local), "ANY", "Empty", 1L, null, null, 1L, 40f, 3f, "6 Month" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PurchasesMasters",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
