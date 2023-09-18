import { Company } from "./company.model";
import { Supplier } from "./supplier.model";

export interface PurchasesMaster{
    id:number;
    userId:number;
    purchasesDate:string;
    purchasesCode:string;
    purchasesType:string;
    supplier:Supplier;
    warrenty:string;
    attn:string;
    lcNumber:number;
    lcDate:string;
    poNumber:number;
    remarks:string;
    company:Company;
    discountAmount:number;
    discountPercent:number;
    vatAmount:number;
    vatPercent:number;
    paymentType:string;
    cancle:boolean;
}