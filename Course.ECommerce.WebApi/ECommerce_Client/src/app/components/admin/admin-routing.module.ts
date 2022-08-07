import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './products/products.component';
import { BrandsComponent } from './brands/brands.component';
import { TypesComponent } from './types/types.component';
import { MainComponent } from './main/main.component';


const routes : Routes = [
  {path: '', component: MainComponent},
  {path:'products',component:ProductsComponent},
  {path:'brands',component:BrandsComponent},
  {path:'types',component:TypesComponent},
];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports:[
    RouterModule
  ]
})
export class AdminRoutingModule { }
