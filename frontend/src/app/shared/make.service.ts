import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Subject } from 'rxjs';
import { Make, MakeViewModel } from './make.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class MakeService implements OnInit {

  makeUrl = 'http://localhost:58151/api/VehicleMake';

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  getMakes(Sort: string, Filter: string, Page: number, pageSize: number, Ascending: string): Observable<MakeViewModel> {
    const params = new HttpParams()
                .set('Sort', Sort)
                .set('Filter', Filter)
                .set('Page', Page.toString())
                .set('pageSize', pageSize.toString())
                .set('Ascending', Ascending);

    console.log(params.toString());

    return this.http.get<MakeViewModel>(this.makeUrl, {params});
  }

  getMake(id: number): Observable<Make> {
    const url = `${this.makeUrl}/${id}`;
    return this.http.get<Make>(url);
  }

  addMake(name, abrv) {
    const obj = {
      name: name,
      abrv: abrv
    };
    return this.http.post(this.makeUrl, obj).subscribe(res => console.log('Done'));
  }

  updateMake(name, abrv, id) {
    const uri = `${this.makeUrl}/${id}`;
    const obj = {
      name: name,
      abrv: abrv
    };
    this.http.put(uri, obj).subscribe(res => console.log('Done'));
  }

  deleteMake(id) {
    return this.http.delete(`${this.makeUrl}/${id}`);
  }
}
