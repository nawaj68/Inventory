import { CategoryCreateUpdateComponent } from 'src/app/modules/admin/category/category-create-update/category-create-update.component';
import { CategoryListComponent } from 'src/app/modules/admin/category/category-list/category-list.component';
import { CityCreateUpdateComponent } from 'src/app/modules/admin/city/city-create-update/city-create-update.component';
import { CityListComponent } from 'src/app/modules/admin/city/city-list/city-list.component';
import { CompanyCreateUpdateComponent } from 'src/app/modules/admin/company/company-create-update/company-create-update.component';
import { CompanyListComponent } from 'src/app/modules/admin/company/company-list/company-list.component';
import { CountryCreateUpdateComponent } from 'src/app/modules/admin/country/country-create-update/country-create-update.component';
import { CountryListComponent } from 'src/app/modules/admin/country/country-list/country-list.component';
import { DamageCreateUpdateComponent } from 'src/app/modules/admin/damage/damage-create-update/damage-create-update.component';
import { DamageListComponent } from 'src/app/modules/admin/damage/damage-list/damage-list.component';
import { DashboardComponent } from 'src/app/modules/admin/dashboard/dashboard.component';
import { PurchasesMasterCreateUpdateComponent } from 'src/app/modules/admin/purchasesMaster/purchases-master-create-update/purchases-master-create-update.component';
import { PurchasesMasterListComponent } from 'src/app/modules/admin/purchasesMaster/purchases-master-list/purchases-master-list.component';
import { ItemCreateUpdateComponent } from 'src/app/modules/admin/item/item-create-update/item-create-update.component';
import { ItemListComponent } from 'src/app/modules/admin/item/item-list/item-list.component';
import { PurchaseDetailsCreateUpdateComponent } from 'src/app/modules/admin/purchase-details/purchase-details-create-update/purchase-details-create-update.component';
import { PurchaseDetailsListComponent } from 'src/app/modules/admin/purchase-details/purchase-details-list/purchase-details-list.component';
import { ReturnCreateUpdateComponent } from 'src/app/modules/admin/return/return-create-update/return-create-update.component';
import { ReturnListComponent } from 'src/app/modules/admin/return/return-list/return-list.component';
import { StateCreateUpdateComponent } from 'src/app/modules/admin/state/state-create-update/state-create-update.component';
import { StateListComponent } from 'src/app/modules/admin/state/state-list/state-list.component';

import { SupplierCreateUpdateComponent } from 'src/app/modules/admin/supplier/supplier-create-update/supplier-create-update.component';
import { SupplierListComponent } from 'src/app/modules/admin/supplier/supplier-list/supplier-list.component';
import { FrontRoute } from './front-route';
import { SubCategoryListComponent } from 'src/app/modules/admin/subCategory/sub-category-list/sub-category-list.component';
import { SubCategoryCreateUpdateComponent } from 'src/app/modules/admin/subCategory/sub-category-create-update/sub-category-create-update.component';
import { UnitListComponent } from 'src/app/modules/admin/unit/unit-list/unit-list.component';
import { UnitCreateUpdateComponent } from 'src/app/modules/admin/unit/unit-create-update/unit-create-update.component';
import { WareHouseListComponent } from 'src/app/modules/admin/wareHouse/ware-house-list/ware-house-list.component';
import { WareHouseCreateUpdateComponent } from 'src/app/modules/admin/wareHouse/ware-house-create-update/ware-house-create-update.component';

const frontRoute = FrontRoute;

export const navs = [

    { path: " ", redirectTo: "dashboard", pathMatch: "full" },
    { name: "Dashboard", path: frontRoute.dashboard, component: DashboardComponent, icon: "dashboard", active: true, title: "", priority: 1, menu: true, data: { breadcrumb: "Dashboard" } },


];

export const settings = [

    { name: "Country", path: frontRoute.countryList, component: CountryListComponent, icon: "public", done: false, menu: true },
    { name: "Country Create", path: frontRoute.countryCreate, component: CountryCreateUpdateComponent, icon: "fas fa-globe", done: true, menu: false },
    { name: "Country Edit", path: frontRoute.countryEdit, component: CountryCreateUpdateComponent, icon: "fas fa-globe", done: true, menu: false },
  
    { name: "State", path: frontRoute.stateList, component: StateListComponent, icon: "map", done: false, menu: true },
    { name: "State Create", path: frontRoute.stateCreate, component: StateCreateUpdateComponent, icon: "fas fa-street-view", done: true, menu: false },
    { name: "State Edit", path: frontRoute.stateEdit, component: StateCreateUpdateComponent, icon: "fas fa-street-view", done: true, menu: false },
  
    { name: "City", path: frontRoute.cityList, component: CityListComponent, icon: "location_city", done: false, menu: true },
    { name: "City Create", path: frontRoute.cityCreate, component: CityCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "City Edit", path: frontRoute.cityEdit, component: CityCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },

    { name: "Category", path: frontRoute.categoryList, component: CategoryListComponent, icon: "category", done: false, menu: true },
    { name: "Category Create", path: frontRoute.categoryCreate, component: CategoryCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "Category Edit", path: frontRoute.categoryEdit, component: CategoryCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },

    { name: "Sub Category", path: frontRoute.subCategoryList, component: SubCategoryListComponent, icon: "schema", done: false, menu: true },
    { name: "Sub Category Create", path: frontRoute.subCategoryCreate, component: SubCategoryCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "Sub Category Edit", path: frontRoute.subCategoryEdit, component: SubCategoryCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },

    

    { name: "Item", path: frontRoute.itemList, component: ItemListComponent, icon: "add_shopping_cart", done: false, menu: true },
    { name: "Item Create", path: frontRoute.itemCreate, component: ItemCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "Item Edit", path: frontRoute.itemEdit, component: ItemCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },


    { name: "Damage", path: frontRoute.damageList, component: DamageListComponent, icon: "delete_forever", done: false, menu: true },
    { name: "Damage Create", path: frontRoute.damageCreate, component: DamageCreateUpdateComponent, icon: "code", done: true, menu: false },
    { name: "Damage Edit", path: frontRoute.damageEdit, component: DamageCreateUpdateComponent, icon: "code", done: true, menu: false },


    { name: "Unit", path: frontRoute.unitList, component: UnitListComponent, icon: "fas fa-city", done: false, menu: true },
    { name: "Unit Create", path: frontRoute.unitCreate, component: UnitCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "Unit Edit", path: frontRoute.unitEdit, component: UnitCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },

    { name: "WareHouse", path: frontRoute.wareHouseList, component: WareHouseListComponent, icon: "fas fa-city", done: false, menu: true },
    { name: "WareHouse Create", path: frontRoute.wareHouseCreate, component: WareHouseCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "WareHouse Edit", path: frontRoute.wareHouseEdit, component: WareHouseCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },

];

export const salesInvoice = [

    
];

export const inventory = [
    { name: "Return", path: frontRoute.returnList, component: ReturnListComponent, icon: "assignment_return", done: false, menu: true },
    { name: "Return Create", path: frontRoute.returnCreate, component: ReturnCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "Return Edit", path: frontRoute.returnEdit, component: ReturnCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },

    { name: "Supplier", path: frontRoute.supplierList, component: SupplierListComponent, icon: "local_shipping", done: false, menu: true },
    { name: "Supplier Create", path: frontRoute.supplierCreate, component: SupplierCreateUpdateComponent, icon: "local_shipping", done: true, menu: false },
    { name: "Supplier Edit", path: frontRoute.supplierEdit, component: SupplierCreateUpdateComponent, icon: "local_shipping", done: true, menu: false },

    { name: "Company", path: frontRoute.companyList, component: CompanyListComponent, icon: "business", done: false, menu: true },
    { name: "Company Create", path: frontRoute.companyCreate, component: CompanyCreateUpdateComponent, icon: "business", done: true, menu: false },
    { name: "Company Edit", path: frontRoute.companyEdit, component: CompanyCreateUpdateComponent, icon: "business", done: true, menu: false },

    { name: "Purchases Master", path: frontRoute.purchasesMasterList, component: PurchasesMasterListComponent, icon: "shop_two", done: false, menu: true },
    { name: "Purchases Master", path: frontRoute.purchasesMasterCreate, component: PurchasesMasterCreateUpdateComponent, icon: "shop_two", done: true, menu: false },
    { name: "Purchases Master", path: frontRoute.purchasesMasterEdit, component: PurchasesMasterCreateUpdateComponent, icon: "shop_two", done: true, menu: false },
    { name: "Purchase-Details", path: frontRoute.purchaseDetailsList, component: PurchaseDetailsListComponent, icon: "fas fa-city", done: false, menu: true },
    { name: "Purchase-Details Create", path: frontRoute.purchaseDetailsCreate, component: PurchaseDetailsCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    { name: "Purchase-Details Edit", path: frontRoute.purchaseDetailsEdit, component: PurchaseDetailsCreateUpdateComponent, icon: "fas fa-city", done: true, menu: false },
    
];