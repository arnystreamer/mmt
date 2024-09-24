import { ResolveFn } from '@angular/router';
import { GlobalCategoriesService } from './global-categories.service';
import { inject } from '@angular/core';
import { GlobalSectionCategory } from '../models/global-section-category.model';
import { CollectionApi } from 'src/app/models/collection-api';

export const globalCategoriesResolver: ResolveFn<CollectionApi<GlobalSectionCategory>> = (route, state) => {
  const globalCategoriesService = inject(GlobalCategoriesService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return { count: 0, skip: 0, take: 0, total: 0, items: [] };
  }

  return globalCategoriesService.getAll(parseInt(sectionId));
};
