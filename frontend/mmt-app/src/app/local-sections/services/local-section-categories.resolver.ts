import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { SectionCategory } from 'src/app/models/sections/section-category.model';
import { LocalSectionCategoriesService } from './local-section-categories.service';
import { inject } from '@angular/core';

export const localSectionCategoriesResolver: ResolveFn<CollectionApi<SectionCategory> | undefined> = (route, state) => {
  const localSectionCategoriesService = inject(LocalSectionCategoriesService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return undefined;
  }

  return localSectionCategoriesService.getAll(parseInt(sectionId));
};
