import { Component, OnInit, TemplateRef } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { AdminService } from '../admin.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ShopService } from '../../shop/shop.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IProductType } from 'src/app/shared/models/productType';
import { IProductBrand } from 'src/app/shared/models/productBrand';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  modalRef?: BsModalRef;
  products:IProduct[];
  types:IProductType[];
  brands:IProductBrand[];
  productIdSelected:string='';
  productForm:FormGroup;
  product:any;
  
  constructor(private adminService:AdminService, private modalService:BsModalService,
             private shopService:ShopService, private formBuilder:FormBuilder, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.getAllProducts();
    this.getProductBrands();
    this.getProductTypes();
  }

  getProduct(template: TemplateRef<any>, id:string) {
    this.shopService.getProductById(id).subscribe((response:IProduct) =>{
      this.product = response;
      this.productIdSelected = this.product.id;
      this.builEditProductForm();
      this.modalRef = this.modalService.show(template);
    }, error => {
      console.log(error);
    })
   
  }

  getAllProducts(){
    this.adminService.getAllProducts().subscribe((response:IProduct[])=>{
      this.products = response ;
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  getProductTypes() {
    this.adminService.getTypes().subscribe((response:IProductType[]) =>{
      this.types = response;
    }, error => {
      console.log(error);
    })
   
  }

  getProductBrands() {
    this.adminService.getBrands().subscribe((response:IProductBrand[]) =>{
      this.brands = response;
    }, error => {
      console.log(error);
    })
   
  }

  openModal(template: TemplateRef<any>){
    this.builNewProductForm();
    this.modalRef = this.modalService.show(template);
  }

  closeModal(){
    this.productIdSelected='';
    this.modalRef?.hide();

  }

  builNewProductForm() {
    this.productForm = this.formBuilder.group({
      name: [null, [Validators.required, Validators.maxLength(100)]],
      description: [null, [Validators.required, Validators.maxLength(256)]],
      price: [null, [Validators.required]],
      type: [null, [Validators.required]],
      brand: [null, [Validators.required]],
    });
  }

  builEditProductForm() {
    console.log(this.product);
    this.productForm = this.formBuilder.group({
      name: [this.product.name, [Validators.required, Validators.maxLength(100)]],
      description: [this.product.description, [Validators.required, Validators.maxLength(256)]],
      price: [this.product.price, [Validators.required]],
      productTypeId: [this.product.productTypeId, [Validators.required]],
      productBrandId: [this.product.productBrandId, [Validators.required]],
    });
  }

  deleteProduct(id:string){
    this.adminService.deleteProduct(id).subscribe((response)=>{
      if(response){
        this.toastr.success('Product delete it!','', {
          timeOut: 1500,
        }).onHidden.subscribe(()=>{
          location.reload();
        })
      }
      
    }, error => {
      console.log(error);
    })
  }

  onSubmit(){
    if(this.productForm.valid){
      if(this.productIdSelected ===''){
        this.adminService.insertProduct(this.productForm.getRawValue()).subscribe((response)=>{
          console.log(response);
          this.toastr.success('Product save it!','', {
            timeOut: 1500,
          }).onHidden.subscribe(()=>{
            location.reload();
          })
        }, error => {
          console.log(error);
        })
      }else{
        this.adminService.updateProduct(this.productIdSelected,this.productForm.getRawValue()).subscribe((response)=>{
          console.log(response);
          this.toastr.success('Product edit it!','', {
            timeOut: 1500,
          }).onHidden.subscribe(()=>{
            location.reload();
          })
          
        }, error => {
          console.log(error);
        })
      }
    }
  }
}
