import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { localSectionCategoriesResolver } from './local-section-categories.resolver';

describe('localSectionCategoriesResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => localSectionCategoriesResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
