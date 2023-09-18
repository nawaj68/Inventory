import { TestBed } from '@angular/core/testing';

import { PurchasesMasterService } from './purchases-master.service';

describe('PurchasesMasterService', () => {
  let service: PurchasesMasterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PurchasesMasterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
