import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { ApiService } from './api.service';
import { NavComponent } from './nav/nav.component';
import { MakeComponent } from './make/make.component';
import { ModelComponent } from './model/model.component';
import { NewMakeComponent } from './make/new-make/new-make.component';
import { NewModelComponent } from './model/new-model/new-model.component';
import { CarsService } from './shared/cars.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditMakeComponent } from './make/edit-make/edit-make.component';

const routes: Routes = [
  { path: '', redirectTo: '/Make', pathMatch: 'full' },
  { path: 'Make', component: MakeComponent },
  { path: 'Model', component: ModelComponent },
  { path: 'Make/New', component: NewMakeComponent },
  { path: 'Model/New', component: NewModelComponent },
  { path: 'Make/Edit/:id', component: EditMakeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MakeComponent,
    ModelComponent,
    NewMakeComponent,
    NewModelComponent,
    EditMakeComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule.forRoot()
  ],
  providers: [ApiService, CarsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
