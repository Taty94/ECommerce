import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Basket, IBasket, IBasketItem, IFinalBasket } from 'src/app/shared/models/basket';
import { BASE_URL } from 'src/app/shared/models/constants';
import { IDeliveryMethod } from 'src/app/shared/models/delivery';
import { IProduct } from 'src/app/shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  //baseUrl = 'https://localhost:44336/api/';
  baseUrl: string = '';
  private basket = new BehaviorSubject<IBasket>(null);
  basket$ = this.basket.asObservable();
  private finalBasket = new BehaviorSubject<IFinalBasket>(null);
  finalBasket$ = this.finalBasket.asObservable();
  delivery = 0;

  constructor(private http: HttpClient, @Inject(BASE_URL) baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  setDeliveryPrice(deliveryMethod: IDeliveryMethod) {
    this.delivery = deliveryMethod.price;
    this.calculateTotals()
  }
  getBasket(id: string) {
    return this.http.get(`${this.baseUrl}/api/Basket?basketId=${id}`)
      .pipe(
        map((basket: IBasket) => {
          this.basket.next(basket);
          this.calculateTotals();
        })
      );
  }

  setBasket(basket: IBasket) {
    return this.http.post(`${this.baseUrl}/api/Basket`, basket).subscribe((response: IBasket) => {
      this.basket.next(response);
      this.calculateTotals();
    }, error => {
      console.log(error);
    })
  }

  createBasket(): IBasket {
    const basket = new Basket();
    localStorage.setItem('basketId', basket.id)
    return basket;
  }

  getCurrentBasket() {
    return this.basket.value;
  }

  addItemToBasket(product: IProduct, quantity = 1) {
    const item: IBasketItem = this.mapProductoToBasketItem(product, quantity);
    const basket = this.getCurrentBasket() ?? this.createBasket();
    basket.items = this.validateEqualItem(basket.items, item, quantity)
    this.setBasket(basket);
  }

  incrementItemQuiantity(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    const index = basket.items.findIndex(i => i.id == item.id);
    basket.items[index].quantity++;
    this.setBasket(basket);
  }

  decrementItemQuiantity(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    const index = basket.items.findIndex(i => i.id == item.id);
    if (basket.items[index].quantity > 1) {
      basket.items[index].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemBasket(item);
    }

  }
  removeItemBasket(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (basket.items.some(i => i.id == item.id)) {
      basket.items = basket.items.filter(i => i.id !== item.id);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }

  deleteBasketLocally(id: string) {
    this.basket.next(null);
    this.finalBasket.next(null);
    localStorage.removeItem('basketId');
  }

  deleteBasket(basket: IBasket) {
    return this.http.delete(`${this.baseUrl}api/Basket?basketId=${basket.id}`).subscribe(() => {
      this.basket.next(null);
      this.finalBasket.next(null);
      localStorage.removeItem('basketId');
    }, error => {
      console.log(error);
    });
  }
  validateEqualItem(items: IBasketItem[], item: IBasketItem, quantity: number): IBasketItem[] {
    const index = items.findIndex(i => i.id == item.id);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  mapProductoToBasketItem(product: IProduct, quantity: number): IBasketItem {
    return {
      id: product.id,
      productName: product.name,
      price: product.price,
      quantity,
      brand: product.productBrand,
      type: product.productType,
    }
  }

  calculateTotals() {
    debugger;
    const basket = this.getCurrentBasket();
    const delivery = this.delivery;
    const subtotal = basket.items.reduce((a, item) => (item.price * item.quantity) + a, 0);
    const total = subtotal + delivery;
    this.finalBasket.next({ delivery, subtotal, total })
  }
}
