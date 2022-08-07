export interface IOrderCreate {
    userEmail: string;
    basketId: string;
    deliveryId: string;
}

export interface ItemsOrdered {
    id: string;
    name: string;
    price: number;
    quantity: number;
}

export interface IOrder {
    id: string;
    description: string;
    price: number;
    itemsOrdered: ItemsOrdered[];
    status: string;
    subtotal: number;
    total: number;
    creationDate: Date;
}