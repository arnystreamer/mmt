import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { globalSectionItemResolver } from './global-section-item.resolver';
import { GlobalSection } from '../models/global-section.model';

describe('globalSectionItemResolver', () => {
  const executeResolver: ResolveFn<GlobalSection | undefined> = (...resolverParameters) =>
      TestBed.runInInjectionContext(() => globalSectionItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
