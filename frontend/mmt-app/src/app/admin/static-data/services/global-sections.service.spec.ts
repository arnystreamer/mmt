import { TestBed } from '@angular/core/testing';

import { GlobalSectionsService } from './global-sections.service';

describe('GlobalSectionsService', () => {
  let service: GlobalSectionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalSectionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
