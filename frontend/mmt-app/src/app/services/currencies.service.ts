import { Injectable } from '@angular/core';
import { Currency } from '../models/static-data/currency.model';
import { CollectionApi } from '../models/collection-api';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CurrenciesService {

  private apiUrl = environment.apiUrl;
  private receiptsUrl = `${this.apiUrl}/currencies`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<Currency>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<Currency>>(this.receiptsUrl, { params: params });
  }
}
