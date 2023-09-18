import { TestBed } from '@angular/core/testing';

import { PurchaseDetailsService } from './purchase-details.service';

describe('PurchaseDetailsService', () => {
  let service: PurchaseDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PurchaseDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
