import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";

@Injectable({
    providedIn:"root"
})

export class LoggedInUserService implements CanActivate{
    
    constructor(private authService:AuthService,
                private router:Router) {
            
    }
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if(!this.authService.isLoggedIn()){
            this.router.navigateByUrl('/login');
            return false;
        }
        
        return true;
    }

}