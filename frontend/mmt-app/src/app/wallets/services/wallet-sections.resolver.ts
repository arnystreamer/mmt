import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { WalletSection } from '../wallet-section.model';
import { WalletSectionsService } from './wallet-sections.service';
import { inject } from '@angular/core';

export const walletSectionsResolver: ResolveFn<CollectionApi<WalletSection> | undefined> = (route, state) => {
  const walletSectionsService = inject(WalletSectionsService);

  var walletId = route.paramMap.get('id');
  if (!walletId)
  {
    return undefined;
  }

  return walletSectionsService.getAll(parseInt(walletId));
}
