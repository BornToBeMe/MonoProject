import { Component, OnInit } from '@angular/core';
import { ModelComponent } from '../model.component';
import { CarsService } from '../../shared/cars.service';

@Component({
  selector: 'app-new-model',
  templateUrl: './new-model.component.html',
  styleUrls: ['./new-model.component.css']
})
export class NewModelComponent implements OnInit {
  model = {};
  models;

  constructor(private carsService: CarsService) { }

  ngOnInit() {
    // this.carsService.getModels().subscribe(res => {
    //   this.models = res;
    // });
  }

}
