import { TestBed } from '@angular/core/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { GlobalSectionsService } from './global-sections.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('GlobalSectionsService', () => {
  let service: GlobalSectionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    service = TestBed.inject(GlobalSectionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
