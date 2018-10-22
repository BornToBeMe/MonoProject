import { Injectable, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Subject } from 'rxjs';
import { Model, ModelViewModel } from './model.model';
import { Make, MakeViewModel } from './make.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class ModelService implements OnInit {

  modelUrl = 'http://localhost:58151/api/VehicleModel';

  constructor(private http: HttpClient) {}

  ngOnInit() {}

  getModels(Sort: string, Filter: string, Page: number, pageSize: number, Ascending: boolean): Observable<ModelViewModel> {
    const params = new HttpParams()
                .set('Sort', Sort)
                .set('Filter', Filter)
                .set('Page', Page.toString())
                .set('pageSize', pageSize.toString())
                .set('Ascending', Ascending.toString());

    console.log(params.toString());

    return this.http.get<ModelViewModel>(this.modelUrl, {params});
  }

  getMakes(Sort: string, Filter: string, Page: number, pageSize: number, Ascending: boolean): Observable<MakeViewModel> {
    const params = new HttpParams()
                .set('Sort', Sort)
                .set('Filter', Filter)
                .set('Page', Page.toString())
                .set('pageSize', pageSize.toString())
                .set('Ascending', Ascending.toString());

    console.log(params.toString());

    return this.http.get<MakeViewModel>('http://localhost:58151/api/VehicleMake', {params});
  }

  getModel(id: number): Observable<Model> {
    const url = `${this.modelUrl}/${id}`;
    return this.http.get<Model>(url);
  }

  addModel(name, abrv, make) {
    const obj = {
      name: name,
      abrv: abrv,
      make: make
    };
    return this.http.post(this.modelUrl, obj).subscribe(res => console.log('Done'));
  }

  updateModel(name, abrv, make, id) {
    const uri = `${this.modelUrl}/${id}`;
    const obj = {
      name: name,
      abrv: abrv,
      make: make
    };
    this.http.put(uri, obj).subscribe(res => console.log('Done'));
  }

  deleteModel(id) {
    return this.http.delete(`${this.modelUrl}/${id}`);
  }
}
