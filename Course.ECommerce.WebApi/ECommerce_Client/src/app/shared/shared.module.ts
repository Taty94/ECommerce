import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { AggregateOrderComponent } from './components/aggregate-order/aggregate-order.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CarouselModule} from 'ngx-bootstrap/carousel';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { DeliveryStepComponent } from './components/delivery-step/delivery-step.component';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    AggregateOrderComponent,
    DeliveryStepComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    CdkStepperModule
  ],
  exports:[
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    AggregateOrderComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    CdkStepperModule,
    DeliveryStepComponent
  ]
})
export class SharedModule { }
