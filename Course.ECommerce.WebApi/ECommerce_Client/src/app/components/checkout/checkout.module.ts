import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { CheckoutComponent } from './checkout.component';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutDeliveryComponent,
    CheckoutSuccessComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule
  ]
})
export class CheckoutModule { }
