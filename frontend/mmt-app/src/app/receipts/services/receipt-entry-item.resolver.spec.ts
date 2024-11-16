import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { receiptEntryItemResolver } from './receipt-entry-item.resolver';

describe('receiptEntryItemResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => receiptEntryItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
