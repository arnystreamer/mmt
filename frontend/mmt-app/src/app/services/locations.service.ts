import { Injectable } from '@angular/core';
import { Location } from '../models/static-data/location.model';
import { CollectionApi } from '../models/collection-api';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationsService {

  private apiUrl = environment.apiUrl;
  private receiptsUrl = `${this.apiUrl}/locations`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<Location>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<Location>>(this.receiptsUrl, { params: params });
  }
}
