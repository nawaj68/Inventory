import { Company } from "./company.model";
import { SubCategory } from "./subCategory.model";
import { Unit } from "./unit.model";

export interface Item{
    id: number;
    subCategoryId:number;
    companyId:number;
    unitId:number;
    itemCode:string;
    itemName:string;
    description:string;
    measure:string;
    measurevalue:number;
    unitPrice:number;
    sellPrice:number;
    oldUnitPrice:number;
    oldSellPrice:number;
    reOrderLevel:string;
    stock:number;

    subCategory: SubCategory;
    company: Company;
    unit: Unit;

}