import { TestBed } from '@angular/core/testing';

import { GlobalCategoriesService } from './global-categories.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('GlobalCategoriesService', () => {
  let service: GlobalCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]});
    service = TestBed.inject(GlobalCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
