import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';

import { MakeService } from '../../shared/make.service';
import { Make } from '../../shared/make.model';

@Component({
  selector: 'app-edit-make',
  templateUrl: './edit-make.component.html',
  styleUrls: ['./edit-make.component.css']
})
export class EditMakeComponent implements OnInit {
  make: Make;
  makeForm: FormGroup;

  constructor(private makeService: MakeService, private router: Router, private route: ActivatedRoute, private fb: FormBuilder) {
    this.createForm();
  }

  createForm() {
    this.makeForm = this.fb.group({
      name: ['', Validators.required],
      abrv: ['', Validators.required]
    });
  }

  updateMake(name, abrv) {
    this.route.params.subscribe(params => {
      this.makeService.updateMake(name, abrv, params['id']);
      this.router.navigate(['Make']);
    });
  }

  ngOnInit() {
     this.route.params.subscribe(params => {
      this.makeService.getMake(params['id']).subscribe(res => {
        this.make = res;
      });
    });
  }
}
