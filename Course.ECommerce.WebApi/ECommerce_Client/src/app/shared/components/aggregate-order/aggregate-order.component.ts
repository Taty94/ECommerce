import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/components/basket/basket.service';
import { IFinalBasket } from '../../models/basket';

@Component({
  selector: 'app-aggregate-order',
  templateUrl: './aggregate-order.component.html',
  styleUrls: ['./aggregate-order.component.scss']
})
export class AggregateOrderComponent implements OnInit {

  aggregateOrder$: Observable<IFinalBasket>;

  constructor(private basketService:BasketService) { }

  ngOnInit(): void {
    this.aggregateOrder$ = this.basketService.finalBasket$;
  }

}
