import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { GlobalSectionsService } from './global-sections.service';
import { GlobalSection } from '../models/global-section.model';

export const globalSectionsResolver: ResolveFn<CollectionApi<GlobalSection>> = (route, state) => {
  const globalSectionsService = inject(GlobalSectionsService);

  return globalSectionsService.getAll();
};
