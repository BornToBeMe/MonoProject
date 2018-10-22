import { Component, OnInit } from '@angular/core';
import { ModelService } from '../shared/model.service';
import { Model } from '../shared/model.model';

@Component({
  selector: 'app-model',
  templateUrl: './model.component.html',
  styleUrls: ['./model.component.css']
})
export class ModelComponent implements OnInit {

  Sort = '';
  Filter = '';
  Page = 1;
  pageSize = 3;
  totalItems;
  Pages;
  Ascending = true;
  models: Model[];

  constructor(private modelService: ModelService) { }

  ngOnInit() {
    this.getModels();
  }

  getModels() {
    this.modelService.getModels(this.Sort, this.Filter, this.Page, this.pageSize, this.Ascending).subscribe(res => {
      this.models = res.Items;
      this.totalItems = res.TotalCount;
      this.Pages = res.TotalPageCount;
      console.log(res);
    });
  }

   pageChange(pageNo) {
    this.Page = pageNo;
    this.getModels();
    console.log(pageNo);
  }

  filter(search) {
    console.log(search);
    this.Filter = search;
    this.getModels();
  }

  changeSize(size) {
    console.log(size);
    this.pageSize = size;
    this.getModels();
  }

  sort(sort) {
    console.log(sort);
    this.Sort = sort;
    this.getModels();
  }

  ascending(ascending) {
    this.Ascending = !ascending;
  }

  deleteModel(id) {
    this.modelService.deleteModel(id).subscribe(res => {
      console.log('Deleted');
      this.getModels();
    });
  }


}
