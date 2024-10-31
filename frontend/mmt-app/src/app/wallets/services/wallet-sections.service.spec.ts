import { TestBed } from '@angular/core/testing';

import { WalletSectionsService } from './wallet-sections.service';

describe('WalletSectionsService', () => {
  let service: WalletSectionsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WalletSectionsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
