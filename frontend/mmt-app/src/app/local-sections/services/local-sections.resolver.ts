import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { inject } from '@angular/core';
import { LocalSectionsService } from './local-sections.service';
import { Section } from 'src/app/models/static-data/section.model';

export const localSectionsResolver: ResolveFn<CollectionApi<Section>> = (route, state) => {
  const localSectionsService = inject(LocalSectionsService);

  return localSectionsService.getAll();
};
