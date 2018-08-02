import { Component, OnInit } from '@angular/core';
import { CarsService } from 'src/app/shared/cars.service';
// import { NgForm } from '../../../../node_modules/@angular/forms';
import Make from '../../shared/make.model';

@Component({
  selector: 'app-new-make',
  templateUrl: './new-make.component.html',
  styleUrls: ['./new-make.component.css']
})
export class NewMakeComponent implements OnInit {

  constructor(public carsService: CarsService) { }

  ngOnInit() {
  }

  /* onSubmit(form: NgForm) {
    this.carsService.postVehicleMake(form.value);
  } */

}
