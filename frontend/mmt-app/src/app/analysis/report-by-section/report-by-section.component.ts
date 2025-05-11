import { Component, ViewChild } from '@angular/core';
import { ReportBySection } from '../models/report-by-section.model';
import { ReportService } from '../services/report.service';
import { ChartComponent } from 'ng-apexcharts';
import { ChartOptions } from '../report-by-month/report-by-month.component';

@Component({
  selector: 'mmt-report-by-section',
  standalone: false,

  templateUrl: './report-by-section.component.html',
  styleUrls: ['./report-by-section.component.scss', '../../list-item-details.scss']
})
export class ReportBySectionComponent {
  public report!: ReportBySection;
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions!: ChartOptions;

  constructor(private reportService: ReportService) {

  }

  ngOnInit(): void {
    this.reportService.getBySection()
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
