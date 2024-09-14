import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { GlobalSectionsService } from './global-sections.service';

describe('GlobalSectionsService', () => {
  let service: GlobalSectionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule
      ]
    });
    service = TestBed.inject(GlobalSectionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
