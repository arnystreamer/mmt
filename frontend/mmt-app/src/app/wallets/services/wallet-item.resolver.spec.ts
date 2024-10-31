import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { walletItemResolver } from './wallet-item.resolver';

describe('walletItemResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => walletItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
