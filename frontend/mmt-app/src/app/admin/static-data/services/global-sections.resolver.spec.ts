import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { globalSectionsResolver } from './global-sections.resolver';
import { CollectionApi } from 'src/app/models/collection-api';
import { ItemWithDescription } from 'src/app/models/item-with-description';

describe('globalSectionsResolver', () => {
  const executeResolver: ResolveFn<CollectionApi<ItemWithDescription>> = (...resolverParameters) =>
      TestBed.runInInjectionContext(() => globalSectionsResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
