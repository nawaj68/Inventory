using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagement.Sql.Migrations
{
    public partial class dataInsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDateTimeUtc", "CreatedUserId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedDateTimeUtc", "UpdatedUserId", "UserName" },
                values: new object[] { 1L, 0, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "admin@gmail.com", true, false, null, "admin@gmail.com", "admin", "123456", "017xxxxxxxx", true, "", false, null, 0L, "admin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Cancel", "CategoryCode", "CategoryName", "CreatedBy", "CreatedDateUtc", "Description", "Picture", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, false, "121", "Electronic Devices", 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "", null, null },
                    { 2L, false, "122", "Electronic Accessories", 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "", null, null }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedDateUtc", "Currency", "Flag", "Name", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, "BD", 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "BDT", "bd", "Bangladesh", null, null },
                    { 2L, "USA", 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "USD", "us", "United States", null, null },
                    { 3L, "UK", 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "GBP", "uk", "United Kingdom", null, null }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "SupplierAddress", "SupplierName", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "12/3,Shyamoli", "Hasan Rahman", null, null },
                    { 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "16/9,Uttara", "Sumon Mia", null, null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "UnitName", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Kilogram", null, null },
                    { 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Pitch", null, null },
                    { 3L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Hali", null, null }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "CountryId", "CreatedBy", "CreatedDateUtc", "Name", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Dhaka", null, null },
                    { 2L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Rajshahi", null, null },
                    { 3L, 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "New York", null, null },
                    { 4L, 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Alabama", null, null }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Cancel", "CategoryId", "CreatedBy", "CreatedDateUtc", "Description", "Picture", "SubCategoryCode", "SubCategoryName", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, false, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "", "100", "Smartphones", null, null },
                    { 2L, false, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "", "101", "Laptops", null, null },
                    { 3L, false, 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "", "102", "Mobile Accessories", null, null },
                    { 4L, false, 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "", "103", "Computer Accessories", null, null }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "Name", "StateId", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Mohammadpur", 1L, null, null },
                    { 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Dhanmondi", 1L, null, null },
                    { 3L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Nator", 2L, null, null },
                    { 4L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Sirajganj", 2L, null, null },
                    { 5L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "New York City", 3L, null, null },
                    { 6L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Buffalo", 3L, null, null },
                    { 7L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Huntsville", 4L, null, null },
                    { 8L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "Montgomery", 4L, null, null }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "CityId", "CompanyName", "ContactNumber", "CountryId", "CreatedBy", "CreatedDateUtc", "StateId", "UpdatedBy", "UpdatedDateUtc", "UserId", "ZipCode" },
                values: new object[,]
                {
                    { 1L, "53/2, Road#2, Housing Ltd.", 1L, "AB ", "017xxxxxxxx", 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), 1L, null, null, null, null },
                    { 2L, "33/12, Road#32, Housing Ltd.", 5L, "XY", "019xxxxxxxx", 2L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), 3L, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CityId", "Contact", "CountryId", "CreatedBy", "CreatedDateUtc", "Email", "Name", "Phone", "StateId", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[,]
                {
                    { 1L, null, 1L, "017xxxxxxxx", 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "sobuj@gmail.com", "Sobuj", "019xxxxxxxx", 1L, null, null },
                    { 2L, null, 1L, "018xxxxxxxx", 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "rana@gmail.com", "Rana", "017xxxxxxxx", 1L, null, null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "CompanyId", "CountryId", "CreatedBy", "CreatedDateUtc", "CurrencyCode", "CurrencyName", "IsDefault", "Syambol", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[] { 1L, 1L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "BDT", "Taka", true, null, null, null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CompanyId", "CreatedBy", "CreatedDateUtc", "Description", "ItemCode", "ItemName", "Measure", "Measurevalue", "OldSellPrice", "OldUnitPrice", "ReOrderLevel", "SellPrice", "Stock", "SubCategoryId", "UnitId", "UnitPrice", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[] { 1L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "s001", "Samsung A53", "", 0.0, 0.0, 0.0, "", 52000.0, 20.0, 1L, 2L, 50000.0, null, null });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CompanyId", "CreatedBy", "CreatedDateUtc", "Description", "ItemCode", "ItemName", "Measure", "Measurevalue", "OldSellPrice", "OldUnitPrice", "ReOrderLevel", "SellPrice", "Stock", "SubCategoryId", "UnitId", "UnitPrice", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[] { 2L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), "", "c002", "Asus", "", 0.0, 0.0, 0.0, "", 82000.0, 20.0, 2L, 2L, 80000.0, null, null });

            migrationBuilder.InsertData(
                table: "Damages",
                columns: new[] { "Id", "CompanyId", "CreatedBy", "CreatedDateUtc", "DamageDate", "DamageQuantity", "DamageReason", "ItemId", "Quantity", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[] { 1L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.0, "", 1L, 20.0, null, null });

            migrationBuilder.InsertData(
                table: "OpenQuantities",
                columns: new[] { "Id", "CreatedBy", "CreatedDateUtc", "ItemId", "OpeningQuentity", "PurchaseQuantity", "ReorderQuantity", "Sell", "UpdatedBy", "UpdatedDateUtc" },
                values: new object[] { 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), 1L, 10.0, 10.0, 0.0, "", null, null });

            migrationBuilder.InsertData(
                table: "Returns",
                columns: new[] { "Id", "CompanyId", "CreatedBy", "CreatedDateUtc", "ItemId", "UpdatedBy", "UpdatedDateUtc", "UserId", "quantity", "reasonOfReturn", "returnDate" },
                values: new object[] { 1L, 1L, 1L, new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 6, 0, 0, 0)), 1L, null, null, 1L, 1.0, "Technical fault", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Damages",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "OpenQuantities",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Returns",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
