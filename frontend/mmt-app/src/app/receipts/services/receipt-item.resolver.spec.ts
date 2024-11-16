import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { receiptItemResolver } from './receipt-item.resolver';

describe('receiptItemResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => receiptItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
