import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ModelService } from '../../shared/model.service';
import { Model } from '../../shared/model.model';
import { Make } from '../../shared/make.model';

@Component({
  selector: 'app-edit-model',
  templateUrl: './edit-model.component.html',
  styleUrls: ['./edit-model.component.css']
})
export class EditModelComponent implements OnInit {

  model: Model;
  modelForm: FormGroup;
  makeList: Make[];

  constructor(private modelService: ModelService, private router: Router, private route: ActivatedRoute, private fb: FormBuilder) {
    this.createForm();
  }

  createForm() {
    this.modelForm = this.fb.group({
      name: ['', Validators.required],
      abrv: ['', Validators.required],
      make: ['', Validators.required]
    });
  }

  updateModel(name, abrv, make) {
    this.route.params.subscribe(params => {
      this.modelService.updateModel(name, abrv, make, params['id']);
      this.router.navigate(['Model']);
    });
  }

  ngOnInit() {
     this.route.params.subscribe(params => {
      this.modelService.getModel(params['id']).subscribe(res => {
        this.model = res;
      });
    });
    this.modelService.getMakes('Name', '', 1, 100, true).subscribe(res => {
      this.makeList = res.Items;
      console.log(res);
    });
  }

}
