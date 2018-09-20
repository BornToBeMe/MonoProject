import { Component, OnInit } from '@angular/core';
import { ModelComponent } from '../model.component';
import { ModelService } from '../../shared/model.service';
import { NgForm, FormGroup, FormBuilder, Validators } from '../../../../node_modules/@angular/forms';
import { Router } from '../../../../node_modules/@angular/router';
import { Model } from '../../shared/model.model';

@Component({
  selector: 'app-new-model',
  templateUrl: './new-model.component.html',
  styleUrls: ['./new-model.component.css']
})
export class NewModelComponent implements OnInit {

  modelForm: FormGroup;
  model: Model[];

  constructor(private modelService: ModelService, private fb: FormBuilder, private router: Router) {
    this.createForm();
  }

  createForm() {
    this.modelForm = this.fb.group({
      name: ['', Validators.required],
      abrv: ['', Validators.required],
      make: ['', Validators.required]
    });
  }

  addModel(name, abrv, make) {
    this.modelService.addModel(name, abrv, make);
    this.router.navigate(['Make']);
  }

  ngOnInit() {
  }

}
