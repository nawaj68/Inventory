import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminRoutingModule } from './admin-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MaterialModule } from 'src/app/shared/material.module';
import { CategoryListComponent } from './category/category-list/category-list.component';
import { CategoryCreateUpdateComponent } from './category/category-create-update/category-create-update.component';
import { SubCategoryCreateUpdateComponent } from './subCategory/sub-category-create-update/sub-category-create-update.component';
import { SubCategoryListComponent } from './subCategory/sub-category-list/sub-category-list.component';
import { CountryListComponent } from './country/country-list/country-list.component';
import { CountryCreateUpdateComponent } from './country/country-create-update/country-create-update.component';
import { StateCreateUpdateComponent } from './state/state-create-update/state-create-update.component';
import { StateListComponent } from './state/state-list/state-list.component';
import { CityCreateUpdateComponent } from './city/city-create-update/city-create-update.component';
import { CityListComponent } from './city/city-list/city-list.component';
import { AvatarPipe } from 'src/app/core/pipe/avatar.pipe';
import { FileUploadModule } from 'ng2-file-upload';

import { FlagPipe } from 'src/app/core/pipe/flag.pipe';
import { PurchasesMasterListComponent } from './purchasesMaster/purchases-master-list/purchases-master-list.component';
import { PurchasesMasterCreateUpdateComponent } from './purchasesMaster/purchases-master-create-update/purchases-master-create-update.component';
import { SupplierCreateUpdateComponent } from './supplier/supplier-create-update/supplier-create-update.component';
import { SupplierListComponent } from './supplier/supplier-list/supplier-list.component';
import { CompanyCreateUpdateComponent } from './company/company-create-update/company-create-update.component';
import { CompanyListComponent } from './company/company-list/company-list.component';

import { ItemCreateUpdateComponent } from './item/item-create-update/item-create-update.component';
import { ItemListComponent } from './item/item-list/item-list.component';
import { DamageListComponent } from './damage/damage-list/damage-list.component';
import { DamageCreateUpdateComponent } from './damage/damage-create-update/damage-create-update.component';
import { ReturnCreateUpdateComponent } from './return/return-create-update/return-create-update.component';
import { ReturnListComponent } from './return/return-list/return-list.component';
import { PurchaseDetailsCreateUpdateComponent } from './purchase-details/purchase-details-create-update/purchase-details-create-update.component';
import { PurchaseDetailsListComponent } from './purchase-details/purchase-details-list/purchase-details-list.component';
import { UnitListComponent } from './unit/unit-list/unit-list.component';
import { UnitCreateUpdateComponent } from './unit/unit-create-update/unit-create-update.component';
import { WareHouseListComponent } from './wareHouse/ware-house-list/ware-house-list.component';
import { WareHouseCreateUpdateComponent } from './wareHouse/ware-house-create-update/ware-house-create-update.component';



@NgModule({
  declarations: [
    DashboardComponent,
    CategoryListComponent,
    CategoryCreateUpdateComponent,
    SubCategoryCreateUpdateComponent,
    SubCategoryListComponent,
    CountryListComponent,
    CountryCreateUpdateComponent,
    StateCreateUpdateComponent,
    StateListComponent,
    CityCreateUpdateComponent,
    CityListComponent,
    AvatarPipe,
    FlagPipe,
    PurchasesMasterListComponent,
    PurchasesMasterCreateUpdateComponent,
    SupplierCreateUpdateComponent,
    SupplierListComponent,
    CompanyCreateUpdateComponent,
    CompanyListComponent,
    
    FlagPipe,
    ItemCreateUpdateComponent,
    ItemListComponent,
    ItemListComponent,
    DamageListComponent,
    DamageCreateUpdateComponent,
    UnitListComponent,
    UnitCreateUpdateComponent,
    DamageListComponent,
    DamageCreateUpdateComponent,
    ItemListComponent,
    AvatarPipe,
    ReturnCreateUpdateComponent,
    ReturnListComponent,
    PurchaseDetailsCreateUpdateComponent,
    PurchaseDetailsListComponent,

    WareHouseListComponent,
    WareHouseCreateUpdateComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule,
    FileUploadModule
  ]
})
export class AdminModule { }
