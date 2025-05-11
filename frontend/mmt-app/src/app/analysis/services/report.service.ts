import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReportBySection } from '../models/report-by-section.model';
import { ReportBySectionAndCategory } from '../models/report-by-section-and-category.model';
import { ReportByMonth } from '../models/report-by-month.model';
import { ReportByLocation } from '../models/report-by-location.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
    private apiUrl = environment.apiUrl;
    private reportUrl = `${this.apiUrl}/report`;

  constructor(
    private httpClient: HttpClient) { }

  getBySection(startDate?: Date, endDate?: Date) : Observable<ReportBySection>
  {
        let params = new HttpParams();
        if (startDate)
          params = params.set("skip", new Date(startDate).toISOString());

        if (endDate)
          params = params.set("take", new Date(endDate).toISOString());

        return this.httpClient.get<ReportBySection>(`${this.reportUrl}/by-section`, { params: params });
  }

  getBySectionAndCategory(startDate?: Date, endDate?: Date) : Observable<ReportBySectionAndCategory>
  {
    let params = new HttpParams();
    if (startDate)
      params = params.set("skip", new Date(startDate).toISOString());

    if (endDate)
      params = params.set("take", new Date(endDate).toISOString());

    return this.httpClient.get<ReportBySectionAndCategory>(`${this.reportUrl}/by-section-and-category`, { params: params });
  }

  getByMonth(startDate?: Date, endDate?: Date)  : Observable<ReportByMonth>
  {
    let params = new HttpParams();
    if (startDate)
      params = params.set("skip", new Date(startDate).toISOString());

    if (endDate)
      params = params.set("take", new Date(endDate).toISOString());

    return this.httpClient.get<ReportByMonth>(`${this.reportUrl}/by-month`, { params: params });
  }

  getByLocation(startDate?: Date, endDate?: Date)  : Observable<ReportByLocation>
  {
    let params = new HttpParams();
    if (startDate)
      params = params.set("skip", new Date(startDate).toISOString());

    if (endDate)
      params = params.set("take", new Date(endDate).toISOString());

    return this.httpClient.get<ReportByLocation>(`${this.reportUrl}/by-location`, { params: params });
  }
}
