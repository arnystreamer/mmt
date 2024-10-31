import { TestBed } from '@angular/core/testing';

import { WalletSectionCategoriesService } from './wallet-section-categories.service';

describe('WalletSectionCategoriesService', () => {
  let service: WalletSectionCategoriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WalletSectionCategoriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
