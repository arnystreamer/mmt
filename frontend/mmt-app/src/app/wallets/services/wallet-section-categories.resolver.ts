import { ResolveFn } from '@angular/router';
import { WalletSectionCategoriesService } from './wallet-section-categories.service';
import { CollectionApi } from 'src/app/models/collection-api';
import { SectionCategory } from '../../models/sections/section-category.model';
import { inject } from '@angular/core';

export const walletSectionCategoriesResolver: ResolveFn<CollectionApi<SectionCategory> | undefined> = (route, state) => {
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
