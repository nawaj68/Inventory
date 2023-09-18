import { Item } from "./item.model";
import { PurchasesMaster } from "./purchasesMaster.model";

export interface PurchaseDetails{
    id:number;
    purchasesMasterId:number;
    itemId:number;
    quantity:number;
    unitePrice:number;
    sellPrice:number;
    batchNumber:string;

    item:Item;
    purchasesMaster:PurchasesMaster;
}