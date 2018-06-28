import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { ApiService } from '../api.service';

@Component({
  selector: 'app-make',
  templateUrl: './make.component.html',
})
export class MakeComponent {
  make = {}
  makes

  constructor(private api: ApiService) { }

  ngOnInit() {
    this.api.getMakes().subscribe(res => this.makes = res)
  }

}
