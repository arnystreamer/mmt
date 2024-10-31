import { ResolveFn } from '@angular/router';
import { WalletSection } from '../wallet-section.model';
import { WalletSectionsService } from './wallet-sections.service';
import { inject } from '@angular/core';

export const walletSectionItemResolver: ResolveFn<WalletSection | undefined> = (route, state) => {
  const walletSectionsService = inject(WalletSectionsService);

  var walletId = route.paramMap.get('id');
  if (!walletId)
  {
    return undefined;
  }

  var sectionId = route.paramMap.get('sectionId');
  if (!sectionId)
  {
    return undefined;
  }

  return walletSectionsService.get(parseInt(walletId), parseInt(sectionId));
};
