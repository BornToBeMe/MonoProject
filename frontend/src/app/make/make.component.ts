import { Component, OnInit } from '@angular/core';
import { CarsService } from '../shared/cars.service';
import { Make } from '../shared/make.model';

@Component({
  selector: 'app-make',
  templateUrl: './make.component.html',
})
export class MakeComponent implements OnInit {

  Sort = 'Name';
  Search = '';
  Page = '1';
  pageSize = '3';
  Ascending = 'true';

  makes: Make[];

  constructor(private carsService: CarsService) { }

  ngOnInit() {
    this.carsService.getMakes(this.Sort, this.Search, this.Page, this.pageSize, this.Ascending);
  }

  getMakes() {
    this.carsService.getMakes(this.Sort, this.Search, this.Page, this.pageSize, this.Ascending);
  }

  deleteMake(id) {
    this.carsService.deleteMake(id).subscribe(res => {
      console.log('Deleted');
    });
  }

}
