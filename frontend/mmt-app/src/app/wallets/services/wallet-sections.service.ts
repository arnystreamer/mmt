import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { environment } from 'src/environments/environment';
import { WalletSection } from '../models/wallet-section.model';

@Injectable({
  providedIn: 'root'
})
export class WalletSectionsService {
  private apiUrl = environment.apiUrl;
  private walletSectionUrlResolver = (walletId: number) => `${this.apiUrl}/wallets/${walletId}/sections`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(walletId: number, skip?: number, take?: number): Observable<CollectionApi<WalletSection>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<WalletSection>>(this.walletSectionUrlResolver(walletId), { params: params });
  }

  get(walletId: number, id: number) : Observable<WalletSection>
  {
    return this.httpClient.get<WalletSection>(`${this.walletSectionUrlResolver(walletId)}/${id}`);
  }

  post(walletId: number, item: WalletSection) : Observable<WalletSection>
  {
    return this.httpClient.post<WalletSection>(this.walletSectionUrlResolver(walletId), item);
  }

  put(walletId: number, item: WalletSection) : Observable<WalletSection>
  {
    return this.httpClient.put<WalletSection>(this.walletSectionUrlResolver(walletId), item);
  }

  delete(walletId: number, id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.walletSectionUrlResolver(walletId)}/${id}`)
      .pipe(map(v => true));
  }
}
