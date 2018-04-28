import { TestBed, inject } from '@angular/core/testing';

import { CurrentMessurementService } from './current-measurement.service';

describe('CurrentMeasurementService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CurrentMessurementService]
    });
  });

  it('should be created', inject([CurrentMessurementService], (service: CurrentMessurementService) => {
    expect(service).toBeTruthy();
  }));
});
