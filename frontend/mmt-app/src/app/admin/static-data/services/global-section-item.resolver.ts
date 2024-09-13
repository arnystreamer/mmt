import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { GlobalSectionsService } from './global-sections.service';


export const globalSectionItemResolver: ResolveFn<ItemWithDescription | undefined> = (route, state) => {
  const globalSectionsService = inject(GlobalSectionsService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return undefined;
  }

  return globalSectionsService.get(parseInt(sectionId));
};
