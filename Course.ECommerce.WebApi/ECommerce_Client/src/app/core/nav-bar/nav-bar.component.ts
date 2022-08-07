import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/components/account/account.service';
import { BasketService } from 'src/app/components/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IUserInfo } from 'src/app/shared/models/userInfo';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {

  basket$ : Observable<IBasket>;
  userInfo$ : Observable<IUserInfo>;

  constructor(private basketService : BasketService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.userInfo$ = this.accountService.userInfo$;
  }

  logout(){
    this.accountService.logout();
  }

}
