import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private route:Router, private  toastr:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    
    return next.handle(request).pipe(
      catchError(error => {
        if(error) {

          if(error.status === 401) {
            this.toastr.error('you need to log in to access', error.status)
            this.route.navigateByUrl('account/login');
          }

          if(error.status === 400 ) {
            if(error.error.errors.id) this.toastr.error(error.error.errors.id[0], error.status)
            else this.toastr.error('You have made a bad request', error.status)
          }
          
          if(error.status === 404 ) {
            if(error.error.detail) this.toastr.error(error.error.detail, error.status)
            else this.toastr.error('Resouce not Found', error.error.status)
          }
          if(error.status === 500 ) {
            if(error.message.includes('Token')) this.toastr.error('Email or Password Incorrectos') 
            else this.toastr.error('Internal Server Error',error.status)
          }
        }

        return throwError(error);
      })
    )
  }
}
