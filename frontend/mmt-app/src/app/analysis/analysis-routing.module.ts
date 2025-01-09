import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnalysisComponent } from './analysis.component';
import { ReportByMonthComponent } from './report-by-month/report-by-month.component';
import { ReportBySectionComponent } from './report-by-section/report-by-section.component';
import { ReportBySectionAndCategoryComponent } from './report-by-section-and-category/report-by-section-and-category.component';
import { ReportByLocationComponent } from './report-by-location/report-by-location.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AnalysisComponent
  },
  {
    path: 'by-month',
    pathMatch: 'full',
    component: ReportByMonthComponent
  },
  {
    path: 'by-section',
    pathMatch: 'full',
    component: ReportBySectionComponent
  },
  {
    path: 'by-section-and-category',
    pathMatch: 'full',
    component: ReportBySectionAndCategoryComponent
  },
  {
    path: 'by-location',
    pathMatch: 'full',
    component: ReportByLocationComponent
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AnalysisRoutingModule { }
