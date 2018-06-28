import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http'
import { Subject } from "rxjs";

@Injectable()
export class ApiService {
    constructor(private http: HttpClient) {}

    getMakes(){
        return this.http.get(`http://localhost:54253/api/Makes`);
    }
}