import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { LoginService as AuthService } from '../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder,private AuthService:AuthService,private router:Router) {
    this.loginForm = this.fb.group({
      username: ['Admin', [Validators.required, Validators.minLength(4)]],
      password: ['Password', [Validators.required, Validators.minLength(6)]],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      console.log('Form submitted:', this.loginForm.value);
      this.AuthService.login({username:this.loginForm.controls['username'].value,password:this.loginForm.controls['password'].value})
      .subscribe(()=>{
        this.router.navigate(['/dashboard/']);
      });
    } else {
      console.log('Form is invalid');
    }
  }
}
