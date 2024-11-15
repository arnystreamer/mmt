import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { environment } from 'src/environments/environment';
import { SectionCategory } from '../../models/sections/section-category.model';
import { SectionCategoryEdit } from 'src/app/models/sections/section-category-edit.model';

@Injectable({
  providedIn: 'root'
})
export class WalletSectionCategoriesService {
  private apiUrl = environment.apiUrl;
  private walletSectionCategoryUrlResolver = (walletId: number, sectionId: number) => `${this.apiUrl}/wallets/${walletId}/sections/${sectionId}/categories`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(walletId: number, sectionId: number, skip?: number, take?: number): Observable<CollectionApi<SectionCategory>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<SectionCategory>>(this.walletSectionCategoryUrlResolver(walletId, sectionId), { params: params });
  }

  get(walletId: number, sectionId: number, id: number) : Observable<SectionCategory>
  {
    return this.httpClient.get<SectionCategory>(`${this.walletSectionCategoryUrlResolver(walletId, sectionId)}/${id}`);
  }

  post(walletId: number, sectionId: number, item: SectionCategoryEdit) : Observable<SectionCategory>
  {
    return this.httpClient.post<SectionCategory>(this.walletSectionCategoryUrlResolver(walletId, sectionId), item);
  }

  delete(walletId: number, sectionId: number, id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.walletSectionCategoryUrlResolver(walletId, sectionId)}/${id}`)
      .pipe(map(v => true));
  }
}
