import { inject } from '@angular/core';
import { ResolveFn } from '@angular/router';
import { GlobalSectionsService } from './global-sections.service';
import { Section } from 'src/app/models/static-data/section.model';


export const globalSectionItemResolver: ResolveFn<Section | undefined> = (route, state) => {
  const globalSectionsService = inject(GlobalSectionsService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return undefined;
  }

  return globalSectionsService.get(parseInt(sectionId));
};
