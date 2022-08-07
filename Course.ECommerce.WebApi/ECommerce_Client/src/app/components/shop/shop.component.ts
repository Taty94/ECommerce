import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PaginationParams } from 'src/app/shared/models/paginationParams';
import { IProduct } from 'src/app/shared/models/product';
import { IProductBrand } from 'src/app/shared/models/productBrand';
import { IProductType } from 'src/app/shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  @ViewChild('search', {static:true}) search : ElementRef;
  products : IProduct[];
  brands : IProductBrand[];
  types : IProductType[];
  paginationParams : PaginationParams = new PaginationParams();
  itemsPerPage:number;
  total:number;
  sortOptions = [
    {value:"name-asc", detail:'Name: ascending'},
    {value:"name-desc", detail:'Name: descending'},
    {value:"price-asc", detail:'Price: Low to High'},
    {value:"price-desc", detail:'Price: High to Low'},
  ]

  constructor(private shopService:ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.paginationParams).subscribe(response => {
      this.products = response.items;
      this.total = response.total;
      this.itemsPerPage = response.items.length;
    }, error => {
      console.log(error);
    })
  }

  getBrands(){
    this.shopService.getBrands().subscribe(response => {
      this.brands = [{id:'0',description:'All', isDeleted:false}, ...response];
    }, error => {
      console.log(error);
    })
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id:'0',description:'All', isDeleted:false}, ...response];;
    }, error => {
      console.log(error);
    })
  }

  onBrandSelected(brandId: string){
    this.paginationParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: string){
    this.paginationParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(sortValue : string){
    this.paginationParams.sort = sortValue.split('-')[0];
    this.paginationParams.order = sortValue.split('-')[1];
    this.getProducts();
  }

  onPageChanged(event:any){
    this.paginationParams.offset=event;
    this.getProducts();
  }

  onSearch(){
    this.paginationParams.search = this.search.nativeElement.value;
    this.getProducts();
  }

  onClean(){
    this.search.nativeElement.value='';
    this.paginationParams = new PaginationParams();
    this.getProducts();
  }
}
