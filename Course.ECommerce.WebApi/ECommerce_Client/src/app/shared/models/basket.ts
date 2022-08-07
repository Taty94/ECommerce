import { v4 as uuidv4 } from 'uuid';

export interface IBasketItem {
    id: string;
    productName: string;
    price: number;
    quantity: number;
    brand: string;
    type: string;
}

export interface IBasket {
    id: string;
    items: IBasketItem[];
}

export class Basket implements IBasket {
    id = uuidv4();
    items: IBasketItem[]=[];
}

export interface IFinalBasket  {
    delivery:number;
    subtotal:number;
    total:number;
}