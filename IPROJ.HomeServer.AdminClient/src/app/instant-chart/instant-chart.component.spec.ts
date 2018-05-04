import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { InstantChartComponent } from './instant-chart.component';

describe('InstantChartComponent', () => {
  let component: InstantChartComponent;
  let fixture: ComponentFixture<InstantChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InstantChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InstantChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
