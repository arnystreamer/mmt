import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { localSectionsResolver } from './local-sections.resolver';

describe('localSectionsResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => localSectionsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
