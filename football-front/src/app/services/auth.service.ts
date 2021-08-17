import { environment } from './../../environments/environment';
import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ILogin } from '../models/login.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn : 'root'
})
export class AuthService{
    currentUserToken:BehaviorSubject<any> =
        new BehaviorSubject<any>(null);
    email:string;
    role:string;        

    constructor(private http:HttpClient,
                private router:Router,
                private jwtService:JwtHelperService) {
    }

    isLoggedIn(){
        return !this.jwtService.isTokenExpired(this.currentUserToken.value);    
    }

    login(loginModel:ILogin) :Observable<string>{
        return this.http.post<string>(`${environment.baseUrl}auth/login`,loginModel)
            .pipe(
                tap((res:any) =>{
                    this.currentUserToken.next(res.token);
                        this.currentUserToken.next(res.token);
                        const decodedToken = this.jwtService.decodeToken(res.token);
                        this.email = decodedToken.email;
                        this.role = decodedToken.role;
                        localStorage.setItem('footballToken',res.token);
                })
            );
    }

    logout(){
        localStorage.removeItem('footballToken');
        this.currentUserToken.next(null);
        this.router.navigateByUrl('/');
    }
}
