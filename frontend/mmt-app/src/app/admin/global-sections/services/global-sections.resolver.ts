import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { GlobalSectionsService } from './global-sections.service';
import { Section } from 'src/app/models/static-data/section.model';

export const globalSectionsResolver: ResolveFn<CollectionApi<Section>> = (route, state) => {
  const globalSectionsService = inject(GlobalSectionsService);

  return globalSectionsService.getAll();
};
