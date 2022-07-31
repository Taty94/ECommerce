import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'ECommerce_Client';

  products:IProduct[] = [];

  constructor(private http: HttpClient){  }
  
  ngOnInit(): void {
    this.http.get("https://localhost:44336/api/Product?offset=0&limit=50&sort=Name&order=asc")
    .subscribe((response:IPagination)=>{
      this.products = response.items;
      console.log(this.products);
    },error => {
      console.log(error);
    })
  }
}
