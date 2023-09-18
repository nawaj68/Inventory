import { Company } from "./company.model";
import { Item } from "./item.model";

export interface Return{
    id:number;
    companyId:number;
    itemId:number;
    quantity:number;
    reasonOfReturn:string;
    returnDate:string;

    item:Item;
    company:Company;
}
