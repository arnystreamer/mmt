import { ResolveFn } from '@angular/router';
import { LocalSectionsService } from './local-sections.service';
import { inject } from '@angular/core';
import { Section } from 'src/app/models/static-data/section.model';

export const localSectionItemResolver: ResolveFn<Section | undefined> = (route, state) => {
  const localSectionsService = inject(LocalSectionsService);

  var sectionId = route.paramMap.get('id');
  if (!sectionId)
  {
    return undefined;
  }

  return localSectionsService.get(parseInt(sectionId));
};
