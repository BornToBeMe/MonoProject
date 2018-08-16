import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Subject } from 'rxjs';
import { Make } from './make.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class CarsService implements OnInit {

  makeUrl = 'http://localhost:58151/api/VehicleMake';

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  getMakes(Sort: string, Search: string, Page: string, pageSize: string, Ascending: string): Observable<Make[]> {
    const params = new HttpParams()
                .set('Sort', Sort)
                .set('Search', Search)
                .set('Page', Page)
                .set('pageSize', pageSize)
                .set('Ascending', Ascending);

    console.log(params.toString());

    return this.http.get<Make[]>(this.makeUrl, {params});
  }

  getMake(id: number): Observable<Make> {
    const url = `${this.makeUrl}/${id}`;
    return this.http.get<Make>(url);
  }

  addMake(name, abrv) {
    const uri = 'http://localhost:58151/api/VehicleMake';
    const obj = {
      name: name,
      abrv: abrv
    };
    return this.http.post(uri, obj).subscribe(res => console.log('Done'));
  }

  editMake(id) {
    const uri = `http://localhost:58151/api/VehicleMake/${id}`;
    return this.http.get(uri).pipe(map(res => {
      return res;
    }));
  }

  updateMake(name, abrv, id) {
    const uri = `http://localhost:58151/api/VehicleMake/${id}`;
    const obj = {
      name: name,
      abrv: abrv
    };
    this.http.put(uri, obj).subscribe(res => console.log('Done'));
  }

  deleteMake(id) {
    return this.http.delete(`http://localhost:58151/api/VehicleMake/${id}`);
  }
}
