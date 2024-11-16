import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { WalletEdit } from 'src/app/models/static-data/wallet-edit.model';
import { Wallet } from 'src/app/models/static-data/wallet.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class WalletsService {

  private apiUrl = environment.apiUrl;
  private walletsUrl = `${this.apiUrl}/wallets`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<Wallet>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<Wallet>>(this.walletsUrl, { params: params });
  }

  get(id: number) : Observable<Wallet>
  {
    return this.httpClient.get<Wallet>(`${this.walletsUrl}/${id}`);
  }

  post(item: WalletEdit) : Observable<Wallet>
  {
    return this.httpClient.post<Wallet>(this.walletsUrl, item);
  }

  delete(id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.walletsUrl}/${id}`)
      .pipe(map(v => true));
  }
}
