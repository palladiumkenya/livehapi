import { TestBed, inject } from '@angular/core/testing';

import { ClientContactsService } from './client-contacts.service';

describe('ClientContactsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientContactsService]
    });
  });

  it('should be created', inject([ClientContactsService], (service: ClientContactsService) => {
    expect(service).toBeTruthy();
  }));
});
