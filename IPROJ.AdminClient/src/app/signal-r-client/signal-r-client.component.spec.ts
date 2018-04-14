import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalRClientComponent } from './signal-r-client.component';

describe('SignalRClientComponent', () => {
  let component: SignalRClientComponent;
  let fixture: ComponentFixture<SignalRClientComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SignalRClientComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SignalRClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
