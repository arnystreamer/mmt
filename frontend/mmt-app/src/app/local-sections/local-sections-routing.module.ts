import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LocalSectionsComponent } from './local-sections.component';
import { localSectionsResolver } from './services/local-sections.resolver';
import { LocalSectionDetailsComponent } from './local-section-details/local-section-details.component';
import { localSectionItemResolver } from './services/local-section-item.resolver';
import { localSectionCategoriesResolver } from './services/local-section-categories.resolver';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LocalSectionsComponent,
    resolve: { itemsApi: localSectionsResolver },
  },
  {
    path: ':id',
    component: LocalSectionDetailsComponent,
    resolve: { item: localSectionItemResolver, itemsApi: localSectionCategoriesResolver }
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LocalSectionsRoutingModule { }
