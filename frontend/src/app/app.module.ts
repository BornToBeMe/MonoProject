import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {
  MatToolbarModule,
  MatButtonModule,
  MatCardModule,
  MatListModule
} from '@angular/material';
import { RouterModule, Routes } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { ApiService } from './api.service';
import { NavComponent } from './nav/nav.component';
import { MakeComponent } from './make/make.component';
import { ModelComponent } from './model/model.component';

const routes = [
  { path: 'Make', component: MakeComponent },
  { path: 'Model', component: ModelComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MakeComponent,
    ModelComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    MatToolbarModule,
    MatButtonModule,
    MatCardModule,
    MatListModule,
    HttpClientModule
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
