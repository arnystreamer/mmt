import { TestBed } from '@angular/core/testing';

import { ReceiptEntriesService } from './receipt-entries.service';

describe('ReceiptEntriesService', () => {
  let service: ReceiptEntriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReceiptEntriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
