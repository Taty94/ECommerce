import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { IProductBrand } from 'src/app/shared/models/productBrand';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.scss']
})
export class BrandsComponent implements OnInit {

  modalRef?: BsModalRef;
  brands:IProductBrand[];
  brandIdSelected:string='';
  brandForm:FormGroup;
  brand:IProductBrand;
  
  constructor(private adminService:AdminService, private modalService:BsModalService,
    private formBuilder:FormBuilder, private toastr:ToastrService) { }

  ngOnInit(): void {
    this.getBrands();
  }

  getBrand(template: TemplateRef<any>, id:string) {
    this.adminService.getProductBrandById(id).subscribe((response:IProductBrand) =>{
      this.brand = response;
      this.brandIdSelected = this.brand.id;
      this.builEditProductBrandForm();
      this.modalRef = this.modalService.show(template);
    }, error => {
      console.log(error);
    })
   
  }

  getBrands(){
    this.adminService.getBrands().subscribe((response:IProductBrand[])=>{
      this.brands = response ;
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  openModal(template: TemplateRef<any>){
    this.builNewProductBrandForm();
    this.modalRef = this.modalService.show(template);
  }

  closeModal(){
    this.brandIdSelected='';
    this.modalRef?.hide();

  }

  builNewProductBrandForm() {
    this.brandForm = this.formBuilder.group({
      id: [null, [Validators.required, Validators.maxLength(4)]],
      description: [null, [Validators.required, Validators.maxLength(256)]]
    });
  }

  builEditProductBrandForm() {
    this.brandForm = this.formBuilder.group({
      id: [this.brand.id, [Validators.required, Validators.maxLength(4)]],
      description: [this.brand.description, [Validators.required, Validators.maxLength(256)]],
    });
  }

  deleteBrand(id:string){
    this.adminService.deleteProductBrand(id).subscribe((response)=>{
      if(response){
        this.toastr.success('Product Brand delete it!','', {
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
    if(this.brandForm.valid){
      if(this.brandIdSelected ===''){
        this.adminService.insertProductBrand(this.brandForm.getRawValue()).subscribe((response)=>{
          console.log(response);
          this.toastr.success('Product Brand save it!','', {
            timeOut: 1500,
          }).onHidden.subscribe(()=>{
            location.reload();
          })
        }, error => {
          console.log(error);
        })
      }else{
        this.adminService.updateProductBrand(this.brandIdSelected,this.brandForm.getRawValue()).subscribe((response)=>{
          console.log(response);
          this.toastr.success('Product Brand edit it!','', {
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
