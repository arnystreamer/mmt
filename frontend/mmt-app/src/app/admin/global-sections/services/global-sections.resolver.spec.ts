import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { globalSectionsResolver } from './global-sections.resolver';
import { CollectionApi } from 'src/app/models/collection-api';
import { GlobalSection } from '../models/global-section.model';

describe('globalSectionsResolver', () => {
  const executeResolver: ResolveFn<CollectionApi<GlobalSection>> = (...resolverParameters) =>
      TestBed.runInInjectionContext(() => globalSectionsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
