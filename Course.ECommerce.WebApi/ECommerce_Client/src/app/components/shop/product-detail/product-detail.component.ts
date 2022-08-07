import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BasketService } from '../../basket/basket.service';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {
  
  product: IProduct;
  quantity:number=1;
  constructor(private shopService:ShopService, private activatedRoute: ActivatedRoute, private basketService: BasketService ) { }

  ngOnInit(): void {
    this.getProductById();
  }

  getProductById(){
    this.shopService.getProductById(this.activatedRoute.snapshot.paramMap.get('id')).subscribe(product => {
      this.product = product;
      console.log(this.product);
    },error => {
      console.log(error);
    })
  }

  addItemBasket(){
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementBasketItem(){
    this.quantity++;
  }
  
  decrementBasketItem(){
    if(this.quantity>1){
    this.quantity--;
    }
  }
}
