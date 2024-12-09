import { TestBed } from '@angular/core/testing';

import { GlobalCategoriesService } from './global-categories.service';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('GlobalCategoriesService', () => {
  let service: GlobalCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    service = TestBed.inject(GlobalCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
