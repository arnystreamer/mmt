import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { walletSectionsResolver } from './wallet-sections.resolver';

describe('walletSectionsResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => walletSectionsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
