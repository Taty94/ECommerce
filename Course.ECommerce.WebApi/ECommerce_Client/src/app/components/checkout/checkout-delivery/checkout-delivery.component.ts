import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IBasket } from 'src/app/shared/models/basket';
import { IDeliveryMethod } from 'src/app/shared/models/delivery';
import { IOrder } from 'src/app/shared/models/order';
import { IUserInfo } from 'src/app/shared/models/userInfo';
import { AccountService } from '../../account/account.service';
import { BasketService } from '../../basket/basket.service';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {

  @Input() deliveryForm: FormGroup;
  deliveryMethods:IDeliveryMethod[];
  constructor(private checkoutService: CheckoutService, private basketService:BasketService,
    private accountService:AccountService, private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethod().subscribe((dm:IDeliveryMethod[])=>{
      this.deliveryMethods = dm;
    }, error =>{
      console.log(error);
    })
    console.log(this.deliveryForm);
  }

  setDeliveryPrice(deliveryMethod:IDeliveryMethod){
    this.basketService.setDeliveryPrice(deliveryMethod)
  }

  submitOrder(){
    const basket = this.basketService.getCurrentBasket();
    const user = this.accountService.getCurrentUser();
    const orderCreate = this.getOrderCreate(basket,user);
    console.log(orderCreate);
    this.checkoutService.createOrder(orderCreate).subscribe((order:IOrder)=>{
      this.toastr.success('Order created successfully','');
      this.basketService.deleteBasketLocally(basket.id);
      const navigationExtras: NavigationExtras = {state:order}
      this.router.navigate(['checkout/success'], navigationExtras);
    }, error => {
      this.toastr.error(error.message);
      console.log(error);
      
    })
  }

  getOrderCreate(basket: IBasket, user:IUserInfo) {
    return {
      userEmail: user.email,
      basketId: basket.id,
      deliveryId: this.deliveryForm.get('deliveryMethod').value
    }
  }

}
