import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Subject } from 'rxjs';
import Make from './make.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class CarsService implements OnInit {

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  getMakes(): Observable<Make[]> {
    return this.http.get<Make[]>('http://localhost:58151/api/VehicleMake');
  }

  getMake(id: number): Observable<Make> {
    return this.http.get<Make>(`http://localhost:58151/api/VehicleMake/${id}`);
  }

  addMake(name, abrv): Observable<Make> {
    const uri = 'http://localhost:58151/api/VehicleMake';
    const obj = {
      name: name,
      abrv: abrv
    };
    return this.http.post<Make>('http://localhost:58151/api/VehicleMake', {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    });
  }

  editMake(id) {
    const uri = 'http://localhost:58151/api/VehicleMake/' + id;
    return this.http.get(uri).pipe(map(res => {
      return res;
    }));
  }

  updateMake(name, abrv, id) {
    const uri = 'http://localhost:58151/api/VehicleMake';
    const obj = {
      name: name,
      abrv: abrv
    };
    this.http.post(uri, obj).subscribe(res => console.log('Done'));
  }

  deleteMake(id) {
    return this.http.delete(`http://localhost:58151/api/VehicleMake/${id}`);
  }
}
