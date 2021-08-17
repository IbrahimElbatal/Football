import { Component, OnInit } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit{
  
  constructor(private authService:AuthService,private jwtService:JwtHelperService){
  }
  
  ngOnInit(): void {
    let token = localStorage.getItem('footballToken');
    if(token){
      this.authService.currentUserToken.next(token);

      const decodedToken = this.jwtService.decodeToken(token);
      this.authService.email = decodedToken.email;
      this.authService.role = decodedToken.role;
    }
  }
  
}
