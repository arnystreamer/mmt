import { Component, ViewChild } from '@angular/core';
import { ReportBySectionAndCategory } from '../models/report-by-section-and-category.model';
import { ChartComponent } from 'ng-apexcharts';
import { ChartOptions } from '../report-by-month/report-by-month.component';
import { ReportService } from '../services/report.service';

@Component({
  selector: 'mmt-report-by-section-and-category',
  standalone: false,

  templateUrl: './report-by-section-and-category.component.html',
  styleUrls: ['./report-by-section-and-category.component.scss', '../../list-item-details.scss']
})
export class ReportBySectionAndCategoryComponent {
  public report!: ReportBySectionAndCategory;
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions!: ChartOptions;

  constructor(private reportService: ReportService) {

  }

  ngOnInit(): void {
    this.reportService.getBySectionAndCategory()
      .subscribe({
        next: v => {
          this.report = v;

          this.chartOptions = {
            series: [
              {
                name: "basic",
                data: v.items.map(i => i.amount)
              }
            ],
            chart: {
              type: "bar",
              height: 350
            },
            plotOptions: {
              bar: {
                horizontal: true
              }
            },
            dataLabels: {
              enabled: false
            },
            xaxis: {
              categories: v.items.map(i => i.section.name)
            }
          };
        }
      });


  }
}
