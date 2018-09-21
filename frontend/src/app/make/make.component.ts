import { Component, OnInit } from '@angular/core';
import { MakeService } from '../shared/make.service';
import { Make, MakeViewModel } from '../shared/make.model';

@Component({
  selector: 'app-make',
  templateUrl: './make.component.html',
  styleUrls: ['./make.component.css']
})
export class MakeComponent implements OnInit {

  Sort = '';
  Filter = '';
  Page = 1;
  pageSize = 3;
  totalItems;
  Pages;
  Ascending = true;
  makes: Make[];

  constructor(private makeService: MakeService) { }

  ngOnInit() {
    this.getMakes();
  }

  getMakes() {
    this.makeService.getMakes(this.Sort, this.Filter, this.Page, this.pageSize, this.Ascending).subscribe(res => {
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

  sort(sort) {
    console.log(sort);
    this.Sort = sort;
    this.getMakes();
  }

  ascending(ascending) {
    this.Ascending = !ascending;
  }

  deleteMake(id) {
    this.makeService.deleteMake(id).subscribe(res => {
      console.log('Deleted');
      this.getMakes();
    });
  }

}
