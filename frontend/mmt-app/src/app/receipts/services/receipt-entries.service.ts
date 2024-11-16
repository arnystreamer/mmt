import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CollectionApi } from 'src/app/models/collection-api';
import { map, Observable } from 'rxjs';
import { ReceiptEntryEdit } from '../models/receipt-entry-edit.model';

@Injectable({
  providedIn: 'root'
})
export class ReceiptEntriesService {

  private apiUrl = environment.apiUrl;
  private receiptEntriesUrlUrlResolver = (receiptId: string) => `${this.apiUrl}/receipts/${receiptId}/entries`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(receiptId: string, skip?: number, take?: number): Observable<CollectionApi<ReceiptEntry>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<ReceiptEntry>>(this.receiptEntriesUrlUrlResolver(receiptId), { params: params });
  }

  get(receiptId: string, id: string) : Observable<ReceiptEntry>
  {
    return this.httpClient.get<ReceiptEntry>(`${this.receiptEntriesUrlUrlResolver(receiptId)}/${id}`);
  }

  post(receiptId: string, item: ReceiptEntryEdit) : Observable<ReceiptEntry>
  {
    item.createTime = new Date();
    return this.httpClient.post<ReceiptEntry>(this.receiptEntriesUrlUrlResolver(receiptId), item);
  }

  delete(receiptId: string, id: string) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.receiptEntriesUrlUrlResolver(receiptId)}/${id}`)
      .pipe(map(v => true));
  }
}
