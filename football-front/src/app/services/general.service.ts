import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable({
    providedIn :"root"
})
export class GeneralService{
    constructor(private http:HttpClient){

    }

    nationalites(){
        return this.http.get<any>(`${environment.baseUrl}general/nationalities`);
    }
    countries(){
        return this.http.get<any>(`${environment.baseUrl}general/countries`);
    }
   
}