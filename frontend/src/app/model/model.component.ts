import { Component, OnInit } from '@angular/core';
import { CarsService } from '../shared/cars.service';

@Component({
  selector: 'app-model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.css']
})
export class ModelComponent implements OnInit {
  model = {};
  models;

  constructor(private carsService: CarsService) { }

  ngOnInit() {
  //  this.carsService.getModels().subscribe(res => {
  //    this.models = res;
  //  });
  }

}
