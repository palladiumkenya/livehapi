import { TestBed, inject } from '@angular/core/testing';

import { ClientManagerService } from './client-manager.service';

describe('ClientManagerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientManagerService]
    });
  });

  it('should be created', inject([ClientManagerService], (service: ClientManagerService) => {
    expect(service).toBeTruthy();
  }));
});
