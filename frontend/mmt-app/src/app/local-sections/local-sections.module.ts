import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocalSectionsRoutingModule } from './local-sections-routing.module';
import { LocalSectionsComponent } from './local-sections.component';
import { FormControlsModule } from '../shared/form-controls.module';
import { MatIcon, MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    LocalSectionsComponent
  ],
  imports: [
    CommonModule,
    LocalSectionsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class LocalSectionsModule { }
