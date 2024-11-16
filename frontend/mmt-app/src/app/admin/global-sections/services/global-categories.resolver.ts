import { ResolveFn } from '@angular/router';
import { GlobalCategoriesService } from './global-categories.service';
import { inject } from '@angular/core';
import { CollectionApi } from 'src/app/models/collection-api';
import { SectionCategory } from 'src/app/models/sections/section-category.model';

export const globalCategoriesResolver: ResolveFn<CollectionApi<SectionCategory>> = (route, state) => {
  const globalCategoriesService = inject(GlobalCategoriesService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return { count: 0, skip: 0, take: 0, total: 0, items: [] };
  }

  return globalCategoriesService.getAll(parseInt(sectionId));
};
