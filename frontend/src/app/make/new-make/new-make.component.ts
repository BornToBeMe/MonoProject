import { Component, OnInit } from '@angular/core';
import { Make } from '../../shared/make.model';
import { NgForm, FormGroup, FormBuilder, Validators } from '../../../../node_modules/@angular/forms';
import { Router } from '../../../../node_modules/@angular/router';
import { MakeService } from '../../shared/make.service';

@Component({
  selector: 'app-new-make',
  templateUrl: './new-make.component.html',
  styleUrls: ['./new-make.component.css']
})
export class NewMakeComponent implements OnInit {

  makeForm: FormGroup;

  constructor(private makeService: MakeService, private fb: FormBuilder, private router: Router) {
    this.createForm();
  }

  createForm() {
    this.makeForm = this.fb.group({
      name: ['', Validators.required],
      abrv: ['', Validators.required]
    });
  }

  addMake(name, abrv) {
    this.makeService.addMake(name, abrv);
    this.router.navigate(['Make']);
  }

  ngOnInit() {}


}
