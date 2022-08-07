import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { IProductType } from 'src/app/shared/models/productType';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-types',
  templateUrl: './types.component.html',
  styleUrls: ['./types.component.scss']
})
export class TypesComponent implements OnInit {

  modalRef?: BsModalRef;
  types: IProductType[];
  typeIdSelected: string = '';
  typeForm: FormGroup;
  type: IProductType;

  constructor(private adminService: AdminService, private modalService: BsModalService,
    private formBuilder: FormBuilder, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getTypes();
  }

  getType(template: TemplateRef<any>, id: string) {
    this.adminService.getProductTypeById(id).subscribe((response: IProductType) => {
      this.type = response;
      this.typeIdSelected = this.type.id;
      this.builEditProductTypeForm();
      this.modalRef = this.modalService.show(template);
    }, error => {
      console.log(error);
    })

  }

  getTypes() {
    this.adminService.getTypes().subscribe((response: IProductType[]) => {
      this.types = response;
      console.log(response);
    }, error => {
      console.log(error);
    })
  }

  openModal(template: TemplateRef<any>) {
    this.builNewProductTypeForm();
    this.modalRef = this.modalService.show(template);
  }

  closeModal() {
    this.typeIdSelected = '';
    this.modalRef?.hide();

  }

  builNewProductTypeForm() {
    this.typeForm = this.formBuilder.group({
      id: [null, [Validators.required, Validators.maxLength(4)]],
      description: [null, [Validators.required, Validators.maxLength(256)]]
    });
  }

  builEditProductTypeForm() {
    this.typeForm = this.formBuilder.group({
      id: [this.type.id, [Validators.required, Validators.maxLength(4)]],
      description: [this.type.description, [Validators.required, Validators.maxLength(256)]],
    });
  }

  deleteType(id: string) {
    this.adminService.deleteProductType(id).subscribe((response) => {
      if (response) {
        this.toastr.success('Product Type delete it!', '', {
          timeOut: 1500,
        }).onHidden.subscribe(() => {
          location.reload();
        })
      }

    }, error => {
      console.log(error);
    })
  }

  onSubmit() {
    if (this.typeForm.valid) {
      if (this.typeIdSelected === '') {
        this.adminService.insertProductType(this.typeForm.getRawValue()).subscribe((response) => {
          console.log(response);
          this.toastr.success('Product Type save it!', '', {
            timeOut: 1500,
          }).onHidden.subscribe(() => {
            location.reload();
          })
        }, error => {
          console.log(error);
        })
      } else {
        this.adminService.updateProductType(this.typeIdSelected, this.typeForm.getRawValue()).subscribe((response) => {
          console.log(response);
          this.toastr.success('Product Type edit it!', '', {
            timeOut: 1500,
          }).onHidden.subscribe(() => {
            location.reload();
          })

        }, error => {
          console.log(error);
        })
      }
    }
  }

}
