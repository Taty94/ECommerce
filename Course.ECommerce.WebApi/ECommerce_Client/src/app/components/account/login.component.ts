import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from './account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(private accountService: AccountService, 
    private formBuilder: FormBuilder,
    private router:Router) { }

  ngOnInit(): void {
    this.builLoginForm();
  }

  builLoginForm() {
    this.loginForm = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email]],
    password: [null, [Validators.required, /*Validators.pattern('^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$')*/]]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.accountService.getToken(this.loginForm.value).subscribe(()=>{
        this.router.navigateByUrl('/shop');
        this.accountService.getUserLoggedIn(this.loginForm.value['email']).subscribe();
      },error =>{
        console.log(error);
      })
    }
  }

}
