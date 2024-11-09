import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { localSectionItemResolver } from './local-section-item.resolver';

describe('localSectionItemResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => localSectionItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
