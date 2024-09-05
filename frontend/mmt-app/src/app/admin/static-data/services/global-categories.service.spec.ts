import { TestBed } from '@angular/core/testing';

import { GlobalCategoriesService } from './global-categories.service';

describe('GlobalCategoriesService', () => {
  let service: GlobalCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
