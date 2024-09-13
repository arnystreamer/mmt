import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { globalSectionItemResolver } from './global-section-item.resolver';
import { ItemWithDescription } from 'src/app/models/item-with-description';

describe('globalSectionItemResolver', () => {
  const executeResolver: ResolveFn<ItemWithDescription | undefined> = (...resolverParameters) =>
      TestBed.runInInjectionContext(() => globalSectionItemResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
