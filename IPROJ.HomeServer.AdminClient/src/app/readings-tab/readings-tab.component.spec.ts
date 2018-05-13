import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReadingsTabComponent } from './readings-tab.component';

describe('ReadingsTabComponent', () => {
  let component: ReadingsTabComponent;
  let fixture: ComponentFixture<ReadingsTabComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReadingsTabComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReadingsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
