import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GlobalSectionsComponent } from './global-sections/global-sections.component';
import { GlobalSectionDetailsComponent } from './global-sections/global-section-details/global-section-details.component';
import { GlobalSectionsAddComponent } from './global-sections/global-sections-add/global-sections-add.component';
import { globalSectionsResolver } from './services/global-sections.resolver';
import { globalSectionItemResolver } from './services/global-section-item.resolver';
import { globalCategoriesResolver } from './services/global-categories.resolver';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'global-sections'
  },
  {
    path: 'global-sections',
    pathMatch: 'full',
    component: GlobalSectionsComponent,
    resolve: { itemsApi: globalSectionsResolver }
  },
  {
    path: 'global-sections/:id',
    component: GlobalSectionDetailsComponent,
    resolve: { item: globalSectionItemResolver, itemsApi: globalCategoriesResolver }
  },
  {
    path: 'global-sections/create',
    component: GlobalSectionsAddComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StaticDataRoutingModule { }
