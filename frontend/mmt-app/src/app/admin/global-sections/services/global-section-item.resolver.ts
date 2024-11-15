import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { GlobalSectionsService } from './global-sections.service';
import { GlobalSection } from '../models/global-section.model';


export const globalSectionItemResolver: ResolveFn<GlobalSection | undefined> = (route, state) => {
  const globalSectionsService = inject(GlobalSectionsService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return undefined;
  }

  return globalSectionsService.get(parseInt(sectionId));
};
