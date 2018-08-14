import { Component, OnInit } from '@angular/core';
import { CarsService } from '../shared/cars.service';
import Make from '../shared/make.model';

@Component({
  selector: 'app-make',
  templateUrl: './make.component.html',
})
export class MakeComponent implements OnInit {

  makes: Make[];

  constructor(private carsService: CarsService) { }

  ngOnInit() {
    this.carsService.getMakes('Name', '', '', 1, 3, true);
  }

  deleteMake(id) {
    this.carsService.deleteMake(id).subscribe(res => {
      console.log('Deleted');
    });
  }

}
