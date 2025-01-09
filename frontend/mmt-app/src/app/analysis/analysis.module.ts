import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnalysisRoutingModule } from './analysis-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { FormControlsModule } from '../shared/form-controls.module';
import { AnalysisComponent } from './analysis.component';
import { ReportByMonthComponent } from './report-by-month/report-by-month.component';
import { NgApexchartsModule } from "ng-apexcharts";
import { ReportByLocationComponent } from './report-by-location/report-by-location.component';
import { ReportBySectionComponent } from './report-by-section/report-by-section.component';
import { ReportBySectionAndCategoryComponent } from './report-by-section-and-category/report-by-section-and-category.component';



@NgModule({
  declarations: [
    AnalysisComponent,
    ReportByMonthComponent,
    ReportByLocationComponent,
    ReportBySectionComponent,
    ReportBySectionAndCategoryComponent
  ],
  imports: [
    CommonModule,
    AnalysisRoutingModule,
    MatIconModule,
    FormControlsModule,
    NgApexchartsModule
  ]
})
export class AnalysisModule { }
