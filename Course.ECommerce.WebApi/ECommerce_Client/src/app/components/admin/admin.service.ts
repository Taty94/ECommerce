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
export class AdminService {

  baseUrl: string;
  constructor(private http: HttpClient, @Inject(BASE_URL) baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getAllProducts() {
    return this.http.get<IProduct[]>(`${this.baseUrl}/api/Product/all`);
  }

  getTypes() {
    return this.http.get<IProduct[]>(`${this.baseUrl}/api/ProductType`);
  }

  getBrands() {
    return this.http.get<IProduct[]>(`${this.baseUrl}/api/ProductBrand`);
  }


  insertProduct(product:IProduct){
    return this.http.post<IProduct>(`${this.baseUrl}/api/Product`,product);
  }

  updateProduct(id:string,product:IProduct){
    return this.http.put<IProduct[]>(`${this.baseUrl}/api/Product?id=${id}`,product);
  }

  deleteProduct(id:string){
    return this.http.delete<boolean>(`${this.baseUrl}/api/Product/${id}`);
  }

  getProductTypeById(id:string){
    return this.http.get<IProductType>(`${this.baseUrl}/api/ProductType/${id}`);
  }

  getProductBrandById(id:string){
    return this.http.get<IProduct>(`${this.baseUrl}/api/ProductBrand/${id}`);
  }

  insertProductType(productType:IProductType){
    return this.http.post<IProductType>(`${this.baseUrl}/api/ProductType`,productType);
  }

  updateProductType(id:string,productType:IProductType){
    return this.http.put<IProductType[]>(`${this.baseUrl}/api/ProductType?id=${id}`,productType);
  }

  deleteProductType(id:string){
    return this.http.delete<boolean>(`${this.baseUrl}/api/ProductType/${id}`);
  }

  insertProductBrand(productBrand:IProductBrand){
    return this.http.post<IProductBrand>(`${this.baseUrl}/api/ProductBrand`,productBrand);
  }

  updateProductBrand(id:string,productBrand:IProductBrand){
    return this.http.put<IProductBrand[]>(`${this.baseUrl}/api/ProductBrand?id=${id}`,productBrand);
  }

  deleteProductBrand(id:string){
    return this.http.delete<boolean>(`${this.baseUrl}/api/ProductBrand/${id}`);
  }
}
