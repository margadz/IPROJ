import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddingDevicesComponent } from './adding-devices.component';

describe('AddingDevicesComponent', () => {
  let component: AddingDevicesComponent;
  let fixture: ComponentFixture<AddingDevicesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddingDevicesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddingDevicesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
