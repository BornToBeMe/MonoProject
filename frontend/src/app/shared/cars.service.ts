import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Subject } from 'rxjs';
import Make from './make.model';
import { Observable } from 'rxjs';

@Injectable()
export class CarsService implements OnInit {

  public API = 'http://localhost:58151/api';
  public VehicleMake_API = `${this.API}/VehicleMake`;

  selectedMake: Make;

  constructor(private http: HttpClient) {}

  ngOnInit() {}

/*   postVehicleMake(make: Make): Observable<Make> {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<Make>('http://localhost:58151/api/VehicleMake', make, httpOptions);
  } */

  getMakes(): Observable<Array<Make>> {
      return this.http.get<Array<Make>>(this.VehicleMake_API);
  }

  getMake(id: string) {
    return this.http.get(`${this.VehicleMake_API}/${id}`);
  }

  saveMake(make: Make): Observable<Make> {
    let result: Observable<Make>;
    if (make.Id) {
      result = this.http.put<Make>(
        `${this.VehicleMake_API}/${make.Id}`,
        make
      );
    } else {
      result = this.http.post<Make>('http://localhost:58151/api/VehicleMake', make);
    }
    return result;
  }

  removeMake(id: number) {
    return this.http.delete(`${this.VehicleMake_API}/${id.toString()}`);
  }

  /*
  getModels() {
    return this.http.get('http://localhost:58151/api/VehicleModel');
  }

  /*

  postMake(make) {
    this.http.post('http://localhost:58151/api/VehicleMake', make).subscribe(res => {
      console.log(res);
    });
  }

  postModel(model) {
    this.http.post('http://localhost:58151/api/VehicleModel', model).subscribe(res => {
      console.log(res);
    });
  }

  putMake(make) {
    this.http.put(`http://localhost:58151/api/VehicleMake/${make.id}`, make).subscribe(res => {
      console.log(res);
    });
  }

  putModel(model) {
    this.http.put(`http://localhost:58151/api/VehicleModel/${model.id}`, model).subscribe(res => {
      console.log(res);
    });
  } */
}
