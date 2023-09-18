import { Category } from "./category.model";

export interface SubCategory{
    id:number;
    categoryId: number;
    subCategoryName:string;
    subCategoryCode:number;
    description:string;
    picture:string;
    cancel:boolean;

    category:Category;
}