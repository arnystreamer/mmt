import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { walletsResolver } from './wallets.resolver';

describe('walletsResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => walletsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
