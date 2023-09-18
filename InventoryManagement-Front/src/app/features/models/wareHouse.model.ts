import { Company } from "./company.model";
import { Country } from "./country.model";

export interface WareHouse{
    id:number;
    name:string;
    location:string;
    countryId:number;
    companyId:number;

    country:Country
    company:Company;
}