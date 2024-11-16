import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { receiptsResolver } from './receipts.resolver';

describe('receiptsResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => receiptsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
