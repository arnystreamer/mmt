import { Component, OnInit, ViewChild } from '@angular/core';
import { ReportService } from '../services/report.service';
import { ReportByMonth } from '../models/report-by-month.model';

import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
};


@Component({
  selector: 'mmt-report-by-month',
  standalone: false,

  templateUrl: './report-by-month.component.html',
  styleUrls: ['./report-by-month.component.scss', '../../list-item-details.scss']
})
export class ReportByMonthComponent implements OnInit {

  public report!: ReportByMonth;
  @ViewChild("chart") chart!: ChartComponent;
  public chartOptions!: ChartOptions;

  constructor(private reportService: ReportService) {

  }

  ngOnInit(): void {
    this.reportService.getByMonth()
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
              categories: v.items.map(i => this.getMonthName(i.month) + " " + i.year)
            }
          };
        }
      });


  }

  getMonthName(month: number): string{
    var monthNames = ["January", "February", "March", "April", "May", "June",
      "July", "August", "September", "October", "November", "December"
    ];
    return monthNames[month-1];
  }
}
