import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn :"root"
})
export class PlayerService{
    constructor(private http:HttpClient){

    }

    players(teamId){
        return this.http.get<any>(`${environment.baseUrl}Player/team/${teamId}`);
    }
    player(id){
        return this.http.get<any>(`${environment.baseUrl}Player/${id}`);
    }
    edit(player){
        return this.http.put(`${environment.baseUrl}Player/Edit`,player);
    }
    delete(id){
        return this.http.delete(`${environment.baseUrl}Player/${id}`);
    }
}