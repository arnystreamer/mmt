import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { walletSectionItemResolver } from './wallet-section-item.resolver';

describe('walletSectionItemResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => walletSectionItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
