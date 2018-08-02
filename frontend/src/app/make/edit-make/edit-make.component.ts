import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';

import { CarsService } from '../../shared/cars.service';
import Make from '../../shared/make.model';

@Component({
  selector: 'app-edit-make',
  templateUrl: './edit-make.component.html',
  styleUrls: ['./edit-make.component.css']
})
export class EditMakeComponent implements OnInit, OnDestroy {
  make: Make = new Make();

  sub: Subscription;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private carsService: CarsService
  ) { }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        this.carsService.getMake(id).subscribe((make: any) => {
          if (make) {
            this.make = make;
          } else {
            console.log(`Make with id '${id}' not found`);
          }
        });
      }
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  gotoList() {
    this.router.navigate(['/Make']);
  }

  save(form: any) {
    this.carsService.saveMake(form).subscribe(
      result => {
        this.gotoList();
      },
      error => console.error(error)
    );
  }

  remove(id: number) {
    this.carsService.removeMake(id).subscribe(
      result => {
        this.gotoList();
      },
      error => console.error(error)
    );
  }

}
