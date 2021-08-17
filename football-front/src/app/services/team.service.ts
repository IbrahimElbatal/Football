import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn :"root"
})
export class TeamService{
    constructor(private http:HttpClient){

    }

    teams(){
        return this.http.get<any>(`${environment.baseUrl}team/teams`);
    }
    team(id){
        return this.http.get<any>(`${environment.baseUrl}team/${id}`);
    }
    edit(team){
        return this.http.put(`${environment.baseUrl}team/Edit`,team);
    }
    delete(id){
        return this.http.delete(`${environment.baseUrl}team/${id}`);
    }

    create(team){
        return this.http.post(`${environment.baseUrl}CreateTeamWithPlayer
        `,team);
    }
}