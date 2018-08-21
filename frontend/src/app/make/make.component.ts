import { Component, OnInit } from '@angular/core';
import { CarsService } from '../shared/cars.service';
import { Make } from '../shared/make.model';

@Component({
  selector: 'app-make',
  templateUrl: './make.component.html',
})
export class MakeComponent implements OnInit {

  Sort = 'Name';
  Filter = '';
  Page = '2';
  pageSize = '3';
  viewBy = 10;
  totalItems;
  Pages = this.totalItems / this.viewBy;
  Ascending = 'true';
  makes: Make[];

  constructor(private carsService: CarsService) { }

  ngOnInit() {
    this.carsService.getMakes(this.Sort, this.Filter, this.Page, this.pageSize, this.Ascending).subscribe(res => {
      console.log(res);
      this.makes = res;
      this.totalItems = res.length;
    });
  }

  setPage(pageNo) {
    this.Page = pageNo;
  }

  setSize(size) {
    this.pageSize = size;
  }

  getMakes() {
    this.carsService.getMakes(this.Sort, this.Filter, this.Page, this.pageSize, this.Ascending);
  }

  deleteMake(id) {
    this.carsService.deleteMake(id).subscribe(res => {
      console.log('Deleted');
    });
  }

}
