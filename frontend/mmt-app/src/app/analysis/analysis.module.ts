import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisRoutingModule } from './analysis-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { FormControlsModule } from '../shared/form-controls.module';
import { AnalysisComponent } from './analysis.component';



@NgModule({
  declarations: [
    AnalysisComponent
  ],
  imports: [
    CommonModule,
    AnalysisRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class AnalysisModule { }
