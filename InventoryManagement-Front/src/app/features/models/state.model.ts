import { Country } from "./country.model";

export interface State{
    id:number;
    name:string;
    countryId:number;
    country:Country;
}