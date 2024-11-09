import { TestBed } from '@angular/core/testing';

import { LocalSectionCategoriesService } from './local-section-categories.service';

describe('LocalSectionCategoriesService', () => {
  let service: LocalSectionCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalSectionCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
