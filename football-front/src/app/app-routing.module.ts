import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoggedInUserService } from './guards/user.guard';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { PlayerComponent } from './player/player.component';
import { CreateTeamComponent } from './team/create-team/create-team.component';
import { TeamComponent } from './team/team.component';


const routes: Routes = [
  {path :"",component :HomeComponent},
  {path :"login",component :LoginComponent},
  {path :"player/:teamId",component :PlayerComponent,canActivate:[LoggedInUserService]},
  {path :"team",component :TeamComponent,canActivate:[LoggedInUserService]},
  {path :"create-team",component :CreateTeamComponent,canActivate:[LoggedInUserService]},
  {path :"**",redirectTo:"",pathMatch:"full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
