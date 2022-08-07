import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map } from 'rxjs';
import { BASE_URL } from 'src/app/shared/models/constants';
import { IDeliveryMethod } from 'src/app/shared/models/delivery';
import { IOrderCreate } from 'src/app/shared/models/order';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  baseUrl:string='';
  constructor(private http: HttpClient, @Inject(BASE_URL) baseUrl:string) { 
    this.baseUrl=baseUrl;
  }

  getDeliveryMethod(){
    return this.http.get(`${this.baseUrl}/api/Delivery`).pipe(
      map((dm:IDeliveryMethod[])=>{
        return dm.sort((a,b)=> b.price - a.price);
      })
    )
  }

  createOrder(order:IOrderCreate){
    return this.http.post(`${this.baseUrl}/api/Order`,order);
  }

  
}
