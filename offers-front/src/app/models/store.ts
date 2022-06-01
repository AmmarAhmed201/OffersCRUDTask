import { Product } from "./product";

export interface Store{
    id:number;
    name:string;
    address:string;
    products:Product[];
}