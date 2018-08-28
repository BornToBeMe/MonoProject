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
  Page = 1;
  pageSize = 3;
  pageStr = this.Page.toString();
  pageSizeStr = this.pageSize.toString();
  totalItems;
  Pages = this.totalItems / this.pageSize;
  Ascending = 'true';
  previousPage: any;
  makes: Make[];

  constructor(private carsService: CarsService) { }

  ngOnInit() {
    this.getAll();
  }

  getMakes() {
    this.carsService.getMakes(this.Sort, this.Filter, this.pageStr, this.pageSizeStr, this.Ascending).subscribe(res => {
      this.makes = res;
      this.pageStr = this.Page.toString();
      this.pageSizeStr = this.pageSize.toString();
      this.Pages = this.totalItems / this.pageSize;
      console.log(res);
    });
  }

  getAll() {
    this.carsService.getMakes(this.Sort, this.Filter, '1', '10000', this.Ascending).subscribe(res => {
      this.totalItems = res.length;
      this.Pages = this.totalItems / this.pageSize;
      console.log(res);
      this.getMakes();
    });
  }

  pageChange(pageNo) {
    this.Page = pageNo;
    this.pageStr = this.Page.toString();
    this.getMakes();
    console.log(pageNo);
  }

  filter(search) {
    console.log(search);
    this.Filter = search;
    this.getAll();
  }

  changeSize(size) {
    console.log(size);
    this.pageSize = size;
    this.pageSizeStr = size.toString();
    this.getAll();
  }

  deleteMake(id) {
    this.carsService.deleteMake(id).subscribe(res => {
      console.log('Deleted');
    });
  }

}
