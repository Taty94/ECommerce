import { HttpClient, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { BASE_URL } from 'src/app/shared/models/constants';
import { IPagination } from 'src/app/shared/models/pagination';
import { PaginationParams } from 'src/app/shared/models/paginationParams';
import { IProduct } from 'src/app/shared/models/product';
import { IProductBrand } from 'src/app/shared/models/productBrand';
import { IProductType } from 'src/app/shared/models/productType';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  //baseUrl = 'https://localhost:44336/api/'
  //baseUrl = environment.baseUrl;
  baseUrl:string = '';

  constructor(private http: HttpClient, @Inject(BASE_URL) baseUrl:string) {
    this.baseUrl = baseUrl;
  }

  getProducts(pagParams:PaginationParams){
    let params = new HttpParams();

    if(pagParams.brandId !== '0'){
      params = params.append('brandId', pagParams.brandId);
    }

    if(pagParams.typeId !== '0'){
      params = params.append('typeId', pagParams.typeId);
    }

    params = params.append('sort', pagParams.sort);
    
    params = params.append('order', pagParams.order);
    
    params = params.append('limit', pagParams.limit);
    
    params = params.append('offset', pagParams.offset);

    if(pagParams.search){
      params = params.append('search', pagParams.search);
    }

    return this.http.get<IPagination>(`${this.baseUrl}/api/Product`, {observe:'response',params})
      .pipe(
        map(response => {
          return response.body;
        })
      )
  }

  getProductById(id:string){
    return this.http.get<IProduct>(`${this.baseUrl}/api/Product/${id}`);
  }
  getBrands(){
    return this.http.get<IProductBrand[]>(`${this.baseUrl}/api/ProductBrand`);
  }

  getTypes(){
    return this.http.get<IProductType[]>(`${this.baseUrl}/api/ProductType`);
  }
}
