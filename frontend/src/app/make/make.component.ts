import { Component, OnInit } from '@angular/core';
import { CarsService } from '../shared/cars.service';
import { Make, MakeViewModel } from '../shared/make.model';

@Component({
  selector: 'app-make',
  templateUrl: './make.component.html',
})
export class MakeComponent implements OnInit {

  Sort = 'Name';
  Filter = '';
  Page = 1;
  pageSize = 3;
  totalItems;
  Pages;
  Ascending = 'true';
  makes: Make[];

  constructor(private carsService: CarsService) { }

  ngOnInit() {
    this.getMakes();
  }

  getMakes() {
    this.carsService.getMakes(this.Sort, this.Filter, this.Page, this.pageSize, this.Ascending).subscribe(res => {
      this.makes = res.Items;
      this.totalItems = res.TotalCount;
      this.Pages = res.TotalPageCount;
      console.log(res);
    });
  }

   pageChange(pageNo) {
    this.Page = pageNo;
    this.getMakes();
    console.log(pageNo);
  }

  filter(search) {
    console.log(search);
    this.Filter = search;
    this.getMakes();
  }

  changeSize(size) {
    console.log(size);
    this.pageSize = size;
    this.getMakes();
  }

  deleteMake(id) {
    this.carsService.deleteMake(id).subscribe(res => {
      console.log('Deleted');
    });
  }

}
