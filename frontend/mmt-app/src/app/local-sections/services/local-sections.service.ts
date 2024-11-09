import { Injectable } from '@angular/core';
import { LocalSection } from '../models/local-section.model';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocalSectionsService {
  private apiUrl = environment.apiUrl;
  private localSectionUrl = `${this.apiUrl}/local-sections`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<LocalSection>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<LocalSection>>(this.localSectionUrl, { params: params });
  }

  get(id: number) : Observable<LocalSection>
  {
    return this.httpClient.get<LocalSection>(`${this.localSectionUrl}/${id}`);
  }

  post(item: LocalSection) : Observable<LocalSection>
  {
    return this.httpClient.post<LocalSection>(this.localSectionUrl, item);
  }

  put(item: LocalSection) : Observable<LocalSection>
  {
    return this.httpClient.put<LocalSection>(this.localSectionUrl, item);
  }

  delete(id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.localSectionUrl}/${id}`)
      .pipe(map(v => true));
  }
}
