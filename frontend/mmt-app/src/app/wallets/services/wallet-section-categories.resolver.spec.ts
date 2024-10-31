import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { walletSectionCategoriesResolver } from './wallet-section-categories.resolver';

describe('walletSectionCategoriesResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => walletSectionCategoriesResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
