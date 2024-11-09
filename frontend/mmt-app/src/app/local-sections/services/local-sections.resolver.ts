import { ResolveFn } from '@angular/router';
import { LocalSection } from '../models/local-section.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { inject } from '@angular/core';
import { LocalSectionsService } from './local-sections.service';

export const localSectionsResolver: ResolveFn<CollectionApi<LocalSection>> = (route, state) => {
  const localSectionsService = inject(LocalSectionsService);

  return localSectionsService.getAll();
};
