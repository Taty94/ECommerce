import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AccountService } from './components/account/account.service';
import { BasketService } from './components/basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'ECommerce_Client';

  constructor(private basketService: BasketService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.loadBasket();
    this.loadUserLogged();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basketId');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(response => {
        console.log('getting the basket');
      }, error => {
        console.log(error);
      })
    }
  }

  loadUserLogged() {
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.getUserLoggedIn('tatiana94montenegro@gmail.com')
        .subscribe();
    }
  }
}
