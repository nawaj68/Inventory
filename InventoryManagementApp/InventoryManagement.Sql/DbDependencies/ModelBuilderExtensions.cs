using InventoryManagement.Sql.Entities;
using InventoryManagement.Sql.Entities.Configurations;
using InventoryManagement.Sql.Entities.Enrols;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;

namespace InventoryManagement.Sql.DbDependencies
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    Id = 1,
                    Email= "admin@gmail.com",
                    NormalizedEmail = "admin@gmail.com",
                    EmailConfirmed = true,
                    UserName="admin",
                    NormalizedUserName = "admin",
                    AccessFailedCount = 0,
                    ConcurrencyStamp = "",
                    LockoutEnabled= false,
                    PasswordHash ="123456",
                    PhoneNumber="017xxxxxxxx",
                    PhoneNumberConfirmed= true,
                    SecurityStamp = "",
                    TwoFactorEnabled= false
                });
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Bangladesh",
                    Code = "BD",
                    Currency = "BDT",
                    Flag = "bd",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                },
                new Country
                {
                    Id = 2,
                    Name = "United States",
                    Code = "USA",
                    Currency = "USD",
                    Flag = "us",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                },
                new Country
                {
                    Id = 3,
                    Name = "United Kingdom",
                    Code = "UK",
                    Currency = "GBP",
                    Flag = "gb",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<State>().HasData(
            new State
            {
                Id = 1,
                CountryId = 1,
                Name = "Dhaka",
                CreatedBy = 1,
                CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
            },
            new State
            {
                Id = 2,
                CountryId = 1,
                Name = "Rajshahi",
                CreatedBy = 1,
                CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
            },
            new State
            {
                Id = 3,
                CountryId = 2,
                Name = "New York",
                CreatedBy = 1,
                CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
            },
            new State
            {
                Id = 4,
                CountryId = 2,
                Name = "Alabama",
                CreatedBy = 1,
                CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
            });

            modelBuilder.Entity<City>().HasData(
                new City 
                { 
                    Id = 1, 
                    StateId = 1,
                    Name = "Mohammadpur",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                },new City
                {
                    Id = 2,
                    StateId = 1,
                    Name = "Dhanmondi",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                },new City
                {
                    Id = 3,
                    StateId = 2,
                    Name = "Nator",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new City
                {
                    Id = 4,
                    StateId = 2,
                    Name = "Sirajganj",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new City
                {
                    Id = 5,
                    StateId = 3,
                    Name = "New York City",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new City
                {
                    Id = 6,
                    StateId = 3,
                    Name = "Buffalo",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new City
                {
                    Id = 7,
                    StateId = 4,
                    Name = "Huntsville",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new City
                {
                    Id = 8,
                    StateId = 4,
                    Name = "Montgomery",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<Category>().HasData(
                new Category { 
                    Id= 1,
                    CategoryName= "Electronic Devices",
                    CategoryCode="121",
                    Description="",
                    Picture="",
                    Cancel = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                },new Category
                {
                    Id = 2,
                    CategoryName = "Electronic Accessories",
                    CategoryCode = "122",
                    Description = "",
                    Picture = "",
                    Cancel = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { 
                    Id= 1,
                    CategoryId= 1,
                    SubCategoryName= "Smartphones",
                    SubCategoryCode="100",
                    Description ="",
                    Picture ="",
                    Cancel = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new SubCategory
                {
                    Id = 2,
                    CategoryId = 1,
                    SubCategoryName = "Laptops",
                    SubCategoryCode = "101",
                    Description = "",
                    Picture = "",
                    Cancel = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new SubCategory
                {
                    Id = 3,
                    CategoryId = 2,
                    SubCategoryName = "Mobile Accessories",
                    SubCategoryCode = "102",
                    Description = "",
                    Picture = "",
                    Cancel = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                },new SubCategory
                {
                    Id = 4,
                    CategoryId = 2,
                    SubCategoryName = "Computer Accessories",
                    SubCategoryCode = "103",
                    Description = "",
                    Picture = "",
                    Cancel = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier
                {
                    Id= 1,
                    SupplierName= "Hasan Rahman",
                    SupplierAddress="12/3,Shyamoli",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new Supplier
                {
                    Id = 2,
                    SupplierName = "Sumon Mia",
                    SupplierAddress = "16/9,Uttara",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<Unit>().HasData(
                new Unit
                {
                    Id= 1,
                    UnitName= "Kilogram",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new Unit
                {
                    Id = 2,
                    UnitName = "Pitch",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new Unit
                {
                    Id = 3,
                    UnitName = "Hali",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    CompanyName = "AB ",
                    ContactNumber="017xxxxxxxx",
                    Address="53/2, Road#2, Housing Ltd.",
                    CountryId = 1,
                    StateId= 1,
                    CityId= 1,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                    
                }, new Company
                {
                    Id = 2,
                    CompanyName = "XY",
                    ContactNumber = "019xxxxxxxx",
                    Address = "33/12, Road#32, Housing Ltd.",
                    CountryId = 2,
                    StateId = 3,
                    CityId = 5,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)

                });

            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    CountryId= 1,
                    CompanyId= 1,
                    CurrencyName="Taka",
                    CurrencyCode="BDT",
                    IsDefault=true,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<Customer>().HasData(
               new Customer
               {
                   Id = 1,
                   Name= "Sobuj",
                   Email = "sobuj@gmail.com",
                   Contact="017xxxxxxxx",
                   Phone ="019xxxxxxxx",
                   CountryId= 1,
                   StateId= 1,
                   CityId= 1,
                   CreatedBy = 1,
                   CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
               }, new Customer
               {
                   Id = 2,
                   Name = "Rana",
                   Email = "rana@gmail.com",
                   Contact = "018xxxxxxxx",
                   Phone = "017xxxxxxxx",
                   CountryId = 1,
                   StateId = 1,
                   CityId = 1,
                   CreatedBy = 1,
                   CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
               });

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    SubCategoryId = 1,
                    CompanyId= 1,
                    UnitId = 2,
                    ItemName="Samsung A53" ,
                    ItemCode="s001",
                    Description="",
                    Measure="",
                    Measurevalue= 0,
                    UnitPrice= 50000,
                    SellPrice= 52000,
                    OldUnitPrice= 0,
                    OldSellPrice= 0,
                    ReOrderLevel="",
                    Stock=20,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                }, new Item
                {
                    Id = 2,
                    SubCategoryId = 2,
                    CompanyId = 1,
                    UnitId = 2,
                    ItemName = "Asus",
                    ItemCode = "c002",
                    Description = "",
                    Measure = "",
                    Measurevalue = 0,
                    UnitPrice = 80000,
                    SellPrice = 82000,
                    OldUnitPrice = 0,
                    OldSellPrice = 0,
                    ReOrderLevel = "",
                    Stock = 20,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            
            modelBuilder.Entity<Damage>().HasData(
                new Damage
                {
                    Id = 1,
                    ItemId= 1,
                    CompanyId= 1,
                    Quantity= 20,
                    DamageQuantity= 1,
                    DamageDate = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null),
                    DamageReason="",
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<OpenQuantity>().HasData(
                new OpenQuantity
                {
                    Id = 1,
                    ItemId= 1,
                    OpeningQuentity=10,
                    PurchaseQuantity= 10,
                    Sell="",
                    ReorderQuantity= 0,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });

            modelBuilder.Entity<Return>().HasData(
            new Return
            {
                Id = 1,
                UserId=1,
                CompanyId= 1,
                ItemId= 1, 
                quantity=1,
                reasonOfReturn= "Technical fault",
                CreatedBy= 1,
                CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                
            });
            modelBuilder.Entity<WareHouse>().HasData(
            new WareHouse
            {
                Id=1,
                CompanyId = 1,
                CountryId= 1,
                Name= "RedX",
                Location="Dhanmondi",
                CreatedBy = 1,
                CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)

            });
            modelBuilder.Entity<PurchasesMaster>().HasData(
                new PurchasesMaster
                {
                    Id = 1,
                    UserId = 1,
                    CompanyId = 1,
                    SupplierId = 1,
                    PurchasesCode = "AD1045",
                    PurchasesDate = DateTime.Today,
                    PurchasesType = "ANY",
                    Attn = "None",
                    Warrenty = "6 Month",
                    LcNumber = 15603,
                    LcDate = DateTime.Today,
                    PoNumber = 145230,
                    Remarks = "Empty",
                    DiscountAmount = 50,
                    DiscountPercent = 12,
                    VatAmount = 40,
                    VatPercent = 3,
                    PaymentType = "Cash",
                    Cancle = false,
                    CreatedBy = 1,
                    CreatedDateUtc = DateTime.ParseExact("2023-02-01", "yyyy-MM-dd", null)
                });
        }
    }
}
