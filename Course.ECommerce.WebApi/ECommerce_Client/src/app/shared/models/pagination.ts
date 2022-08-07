import { IProduct } from "./product";

export interface IPagination {
    total: number;
    items: IProduct[];
}