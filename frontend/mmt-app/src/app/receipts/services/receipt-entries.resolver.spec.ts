import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { receiptEntriesResolver } from './receipt-entries.resolver';

describe('receiptEntriesResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => receiptEntriesResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
