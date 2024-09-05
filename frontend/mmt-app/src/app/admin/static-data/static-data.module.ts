import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GlobalSectionsComponent } from './global-sections/global-sections.component';
import { GlobalSectionsListComponent } from './global-sections/global-sections-list/global-sections-list.component';
import { GlobalSectionsAddComponent } from './global-sections/global-sections-add/global-sections-add.component';
import { GlobalSectionsListItemComponent } from './global-sections/global-sections-list-item/global-sections-list-item.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'global-sections'
  },
  {
    path: 'global-sections',
    component: GlobalSectionsComponent
  }
];

@NgModule({
  declarations: [
    GlobalSectionsComponent,
    GlobalSectionsListComponent,
    GlobalSectionsAddComponent,
    GlobalSectionsListItemComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [ RouterModule ]
})
export class StaticDataModule { }
