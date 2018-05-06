import { TestBed, inject } from '@angular/core/testing';

import { SignalRMessengerService } from './signal-r-messenger.service';

describe('SignalRMessengerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SignalRMessengerService]
    });
  });

  it('should be created', inject([SignalRMessengerService], (service: SignalRMessengerService) => {
    expect(service).toBeTruthy();
  }));
});
