export class PaginationParams {
    brandId?:string='0';
    typeId?:string='0';
    sort?:string='name';
    order?:string='asc';
    limit:number=10;
    offset:number=0;
    total:number;
    search?:number;
}