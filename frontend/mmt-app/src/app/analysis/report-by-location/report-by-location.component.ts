import { Component, ViewChild } from '@angular/core';
import { ReportByLocation } from '../models/report-by-location.model';
import { ChartComponent } from 'ng-apexcharts';
import { ChartOptions } from '../report-by-month/report-by-month.component';
import { ReportService } from '../services/report.service';

@Component({
  selector: 'mmt-report-by-location',
  standalone: false,

  templateUrl: './report-by-location.component.html',
  styleUrls: ['./report-by-location.component.scss', '../../list-item-details.scss']
})
export class ReportByLocationComponent {
  public report!: ReportByLocation;
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions!: ChartOptions;

  constructor(private reportService: ReportService) {

  }

  ngOnInit(): void {
    this.reportService.getByLocation()
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
              categories: v.items.map(i => i.countryCode)
            }
          };
        }
      });


  }
}
