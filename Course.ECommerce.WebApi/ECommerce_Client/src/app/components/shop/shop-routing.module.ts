import { NgModule } from '@angular/core';
import { ShopComponent } from './shop.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { RouterModule, Routes } from '@angular/router';


const routes : Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: ProductDetailComponent},
];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes) //solo estaran disponibles en nuestro shop.module
  ],
  exports:[
    RouterModule
  ]
  
})
export class ShopRoutingModule { }
