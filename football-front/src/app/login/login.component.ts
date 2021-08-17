import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService:AuthService,
    private fb:FormBuilder,
    private toastr:ToastrService,
    private router:Router,
    private snipper:NgxSpinnerService) { }

    loginFormGroup : FormGroup;
    ngOnInit(): void {
      this.loginFormGroup = this.fb.group({
        'email': ['',[Validators.required,Validators.email]],
        'password' :['',Validators.required],
      });

    }

    login(){
      if(this.loginFormGroup.invalid)
        return;

       this.snipper.show();
       var loginModel = Object.assign({},this.loginFormGroup.value);
       this.authService.login(loginModel)
        .subscribe((res:any)=>{
          console.log(res);
            this.toastr.success('Login Success','Success');
            this.router.navigate(['/team']);
            this.snipper.hide();

        },err=>{
          this.snipper.hide();
          this.toastr.error('Login Failed','Fail');
          console.log(err);
        });
    }

}
