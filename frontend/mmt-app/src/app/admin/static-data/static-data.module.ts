import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { GlobalSectionsComponent } from './global-sections/global-sections.component';
import { GlobalSectionsListComponent } from './global-sections/global-sections-list/global-sections-list.component';
import { GlobalSectionsAddComponent } from './global-sections/global-sections-add/global-sections-add.component';
import { GlobalSectionsListItemComponent } from './global-sections/global-sections-list-item/global-sections-list-item.component';
import { MatDialogModule} from '@angular/material/dialog';
import { GlobalSectionDetailsComponent } from './global-sections/global-section-details/global-section-details.component';
import { StaticDataRoutingModule } from './static-data-routing.module';
import { FormControlsModule } from 'src/app/shared/form-controls.module';
import { GsectionCategoriesListComponent } from './global-section-categories/gsection-categories-list/gsection-categories-list.component';
import { GsectionCategoriesListItemComponent } from './global-section-categories/gsection-categories-list-item/gsection-categories-list-item.component';
import { GsectionCategoriesAddComponent } from './global-section-categories/gsection-categories-add/gsection-categories-add.component';

@NgModule({
  declarations: [
    GlobalSectionsComponent,
    GlobalSectionsListComponent,
    GlobalSectionsAddComponent,
    GlobalSectionsListItemComponent,
    GlobalSectionDetailsComponent,
    GsectionCategoriesListComponent,
    GsectionCategoriesListItemComponent,
    GsectionCategoriesAddComponent
  ],
  imports: [
    CommonModule,
    StaticDataRoutingModule,
    FormControlsModule,
    MatDialogModule
  ],
  exports: [ RouterModule ]
})
export class StaticDataModule { }
