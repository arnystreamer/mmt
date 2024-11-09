import { ResolveFn } from '@angular/router';
import { LocalSection } from '../models/local-section.model';
import { LocalSectionsService } from './local-sections.service';
import { inject } from '@angular/core';

export const localSectionItemResolver: ResolveFn<LocalSection | undefined> = (route, state) => {
  const localSectionsService = inject(LocalSectionsService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return undefined;
  }

  return localSectionsService.get(parseInt(sectionId));
};
