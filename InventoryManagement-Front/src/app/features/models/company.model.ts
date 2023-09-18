import { Country } from "./country.model";
import { State } from 'src/app/features/models/state.model';
import { City } from "./city.model";

export interface Company{
    id:number;
    userId:number;
    companyName:string;
    address:string;
    country:Country;
    state:State;
    city:City;
    zipCode:string;
    contactNumber:string;
}