import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Receipt } from '../models/receipt.model';
import { ReceiptEdit } from '../models/receipt-edit.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { map, Observable } from 'rxjs';
import { ReceiptRequestParams } from '../models/receipt-request-params.model';

@Injectable({
  providedIn: 'root'
})
export class ReceiptsService {

  private apiUrl = environment.apiUrl;
  private receiptsUrl = `${this.apiUrl}/receipts`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(requestParams: ReceiptRequestParams, skip?: number, take?: number): Observable<CollectionApi<Receipt>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    if (requestParams.dateFrom)
      params = params.set("dateFrom", new Date(requestParams.dateFrom).toISOString());

    if (requestParams.dateTo)
      params = params.set("dateTo", new Date(requestParams.dateTo).toISOString());

    if (requestParams.locationId)
      params = params.set("locationId", requestParams.locationId);

    if (requestParams.currencyId)
      params = params.set("currencyId", requestParams.currencyId);

    if (requestParams.sumFrom)
      params = params.set("sumFrom", requestParams.sumFrom);

    if (requestParams.sumTo)
      params = params.set("sumTo", requestParams.sumTo);

    if (requestParams.comment)
      params = params.set("comment", requestParams.comment);

    return this.httpClient.get<CollectionApi<Receipt>>(this.receiptsUrl, { params: params });
  }

  get(id: string) : Observable<Receipt>
  {
    return this.httpClient.get<Receipt>(`${this.receiptsUrl}/${id}`);
  }

  post(item: ReceiptEdit) : Observable<Receipt>
  {
    item.createTime = new Date();
    return this.httpClient.post<Receipt>(this.receiptsUrl, item);
  }

  delete(id: string) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.receiptsUrl}/${id}`)
      .pipe(map(v => true));
  }
}
