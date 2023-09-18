using AutoMapper;
using InventoryManagement.Core;
using InventoryManagement.Service.Models.Configurations;
using InventoryManagement.Sql.Entities.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagement.Sql.Entities.Enrols;
using InventoryManagement.Service.Models.Enrols;
using static InventoryManagement.Sql.Entities.Identities.IdentityModel;
using InventoryManagement.Sql.Entities;

namespace InventoryManagement.Service.Dependency
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryModel>().ForMember(d => d.Picture, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.Picture) ? "" : $"{CommonVariables.AvatarLocation}/{src.Picture}"))
                .ReverseMap();

            CreateMap<SubCategory, SubCategoryModel>().ForMember(d => d.Picture, opts => opts.MapFrom(src => string.IsNullOrEmpty(src.Picture) ? "" : $"{CommonVariables.AvatarLocation}/{src.Picture}"))
                .ReverseMap();

            CreateMap<Unit, UnitModel>().ReverseMap();
            CreateMap<Country, CountryModel>().ReverseMap();
            CreateMap<State, StateModel>().ReverseMap();
            CreateMap<City, CityModel>().ReverseMap();
            CreateMap<Company, CompanyModel>().ReverseMap();
            CreateMap<Currency, CurrencyModel>().ReverseMap();
            CreateMap<Item, ItemModel>().ReverseMap();
            CreateMap<OpenQuantity, OpenQuantityModel>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();

            CreateMap<WareHouse, WareHouseModel>().ReverseMap();
            CreateMap<Supplier,SupplierModel>().ReverseMap();
            CreateMap<PurchasesMaster, PurchasesMasterModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Return, ReturnModel>().ReverseMap();
            CreateMap<Sales, SalesModel>().ReverseMap();
            CreateMap<PurchaseDetails, PurchaseDetailsModel>().ReverseMap();
            CreateMap<Damage, DamageModel>().ReverseMap();
        }
    }
}
