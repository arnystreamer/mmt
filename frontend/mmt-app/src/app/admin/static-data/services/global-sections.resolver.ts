import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { GlobalSectionsService } from './global-sections.service';

export const globalSectionsResolver: ResolveFn<CollectionApi<ItemWithDescription>> = (route, state) => {
  const globalSectionsService = inject(GlobalSectionsService);

  return globalSectionsService.getAll();
};
