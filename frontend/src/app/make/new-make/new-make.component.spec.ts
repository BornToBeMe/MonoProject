import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewMakeComponent } from './new-make.component';

describe('NewMakeComponent', () => {
  let component: NewMakeComponent;
  let fixture: ComponentFixture<NewMakeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewMakeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewMakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
