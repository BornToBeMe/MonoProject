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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditMakeComponent } from './make/edit-make/edit-make.component';
import { EditModelComponent } from './model/edit-model/edit-model.component';
import { MakeService } from './shared/make.service';
import { ModelService } from './shared/model.service';

const routes: Routes = [
  { path: '', redirectTo: '/Make', pathMatch: 'full' },
  { path: 'Make', component: MakeComponent },
  { path: 'Model', component: ModelComponent },
  { path: 'Make/New', component: NewMakeComponent },
  { path: 'Model/New', component: NewModelComponent },
  { path: 'Make/Edit/:id', component: EditMakeComponent },
  { path: 'Model/Edit/:id', component: EditModelComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MakeComponent,
    ModelComponent,
    NewMakeComponent,
    NewModelComponent,
    EditMakeComponent,
    EditModelComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule.forRoot()
  ],
  providers: [ApiService, MakeService, ModelService],
  bootstrap: [AppComponent]
})
export class AppModule { }
