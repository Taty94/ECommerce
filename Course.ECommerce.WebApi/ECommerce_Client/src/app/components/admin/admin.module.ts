import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products/products.component';
import { BrandsComponent } from './brands/brands.component';
import { TypesComponent } from './types/types.component';
import { AdminRoutingModule } from './admin-routing.module';
import { MainComponent } from './main/main.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [
    ProductsComponent,
    BrandsComponent,
    TypesComponent,
    MainComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule,
    ModalModule.forRoot()
  ]
})
export class AdminModule { }
