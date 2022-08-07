import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  loadingRequest = 0;
  constructor(private spinnerService: NgxSpinnerService) { }

  loading(){
    this.loadingRequest++;
    this.spinnerService.show(
      undefined, {
      type:'ball-grid-pulse',
      bdColor:'rgba(255,255,255,0.7)',
      color:'#333333'
      }
    )
  }

  idle(){
    this.loadingRequest--;
    if(this.loadingRequest <=0){
      this.loadingRequest=0;
      this.spinnerService.hide();
    }
  }
}