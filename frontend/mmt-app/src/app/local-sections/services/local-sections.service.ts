import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Section } from 'src/app/models/static-data/section.model';
import { SectionEdit } from 'src/app/models/static-data/section-edit.model';

@Injectable({
  providedIn: 'root'
})
export class LocalSectionsService {
  private apiUrl = environment.apiUrl;
  private localSectionUrl = `${this.apiUrl}/local-sections`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<Section>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<Section>>(this.localSectionUrl, { params: params });
  }

  get(id: number) : Observable<Section>
  {
    return this.httpClient.get<Section>(`${this.localSectionUrl}/${id}`);
  }

  post(item: SectionEdit) : Observable<Section>
  {
    return this.httpClient.post<Section>(this.localSectionUrl, item);
  }

  delete(id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.localSectionUrl}/${id}`)
      .pipe(map(v => true));
  }
}
