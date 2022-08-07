export interface IProduct {
    id: string;
    name: string;
    price: number;
    description: string;
    productType: string;
    productBrand: string;
    isDeleted:boolean;
    creationDate: Date;
    modifiedDate: Date;
}