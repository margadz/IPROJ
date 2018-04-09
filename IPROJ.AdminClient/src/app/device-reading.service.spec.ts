import { TestBed, inject } from '@angular/core/testing';

import { DeviceReadingService } from './device-reading.service';

describe('DeviceReadingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DeviceReadingService]
    });
  });

  it('should be created', inject([DeviceReadingService], (service: DeviceReadingService) => {
    expect(service).toBeTruthy();
  }));
});
