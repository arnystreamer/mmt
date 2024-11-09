import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalSectionsRoutingModule } from './local-sections-routing.module';
import { LocalSectionsComponent } from './local-sections.component';
import { FormControlsModule } from '../shared/form-controls.module';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { LocalSectionsListComponent } from './local-sections-list/local-sections-list.component';
import { LocalSectionDetailsComponent } from './local-section-details/local-section-details.component';
import { LocalSectionCategoriesListComponent } from './local-section-categories-list/local-section-categories-list.component';

@NgModule({
  declarations: [
    LocalSectionsComponent,
    LocalSectionsListComponent,
    LocalSectionDetailsComponent,
    LocalSectionCategoriesListComponent
  ],
  imports: [
    CommonModule,
    LocalSectionsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class LocalSectionsModule { }
