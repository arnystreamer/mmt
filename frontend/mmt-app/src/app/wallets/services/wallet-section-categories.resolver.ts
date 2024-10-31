import { ResolveFn } from '@angular/router';
import { WalletSectionCategoriesService } from './wallet-section-categories.service';
import { CollectionApi } from 'src/app/models/collection-api';
import { WalletSectionCategory } from '../wallet-section-category.model';
import { inject } from '@angular/core';

export const walletSectionCategoriesResolver: ResolveFn<CollectionApi<WalletSectionCategory> | undefined> = (route, state) => {
  const walletSectionCategoriesService = inject(WalletSectionCategoriesService);

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

  return walletSectionCategoriesService.getAll(parseInt(walletId), parseInt(sectionId));
};
