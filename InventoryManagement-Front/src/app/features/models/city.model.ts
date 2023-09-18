import { State } from "./state.model";

export interface City{
    id:number;
    name:string;
    stateId:number;
    state:State;
}