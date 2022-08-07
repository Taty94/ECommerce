import { HttpClient, HttpResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { BASE_URL } from 'src/app/shared/models/constants';
import { IToken } from 'src/app/shared/models/token';
import { IUserCredentials } from 'src/app/shared/models/userCredentials'
import { IUserInfo } from 'src/app/shared/models/userInfo';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl: string = '';
  private token = new BehaviorSubject<IToken>(null);
  token$ = this.token.asObservable();

  private userLoggedIn = new BehaviorSubject<IUserInfo>(null);
  userInfo$ = this.userLoggedIn.asObservable();

  constructor(private http: HttpClient, @Inject(BASE_URL) baseUrl: string,
  private router:Router) {
    this.baseUrl = baseUrl;
  }


  getToken(values: IUserCredentials) {
    return this.http.post(`${this.baseUrl}/api/Token`, values).pipe(
      map((resp: IToken) => {
        if (resp) {
          localStorage.setItem('token', resp.token);
          this.token.next(resp);
        }
      })
    )
  }

  getCurrentUser() {
    return this.userLoggedIn.value;
  }

  getUserLoggedIn(email: string) {
    return this.http.get(`${this.baseUrl}/api/LocationInfo?email=${email}`).pipe(
      map((user: IUserInfo) => {
        if(user){
          this.userLoggedIn.next(user);
        }
      })
    )
  }

  logout(){
    localStorage.removeItem('token');
    this.userLoggedIn.next(null);
    this.router.navigateByUrl('/');
  }


}
