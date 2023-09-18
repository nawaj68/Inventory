import { Company } from "./company.model";
import { Item } from "./item.model";

export interface Damage{
    id:number;
    itemId:number;
    quantity:number;
    damageQuantity:number;
    damageReason:string;
    companyId:number;
    damageDate:string;

    item:Item;
    company:Company;
}