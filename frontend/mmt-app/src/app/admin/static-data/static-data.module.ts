import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { GlobalSectionsComponent } from './global-sections/global-sections.component';
import { GlobalSectionsListComponent } from './global-sections/global-sections-list/global-sections-list.component';
import { GlobalSectionsAddComponent } from './global-sections/global-sections-add/global-sections-add.component';
import { GlobalSectionsListItemComponent } from './global-sections/global-sections-list-item/global-sections-list-item.component';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GlobalSectionDetailsComponent } from './global-sections/global-section-details/global-section-details.component';

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
  },
  {
    path: 'global-sections/:id',
    component: GlobalSectionDetailsComponent
  },
  {
    path: 'global-sections/create',
    component: GlobalSectionsAddComponent
  }
];

@NgModule({
  declarations: [
    GlobalSectionsComponent,
    GlobalSectionsListComponent,
    GlobalSectionsAddComponent,
    GlobalSectionsListItemComponent,
    GlobalSectionDetailsComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    RouterModule.forChild(routes)
  ],
  exports: [ RouterModule ]
})
export class StaticDataModule { }
