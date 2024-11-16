import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { SectionEdit } from 'src/app/models/static-data/section-edit.model';
import { Section } from 'src/app/models/static-data/section.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GlobalSectionsService {

  private apiUrl = environment.apiUrl;
  private globalSectionsUrl = `${this.apiUrl}/global-sections`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<Section>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<Section>>(this.globalSectionsUrl, { params: params });
  }

  get(id: number) : Observable<Section>
  {
    return this.httpClient.get<Section>(`${this.globalSectionsUrl}/${id}`);
  }

  post(item: SectionEdit) : Observable<Section>
  {
    return this.httpClient.post<Section>(this.globalSectionsUrl, item);
  }

  delete(id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.globalSectionsUrl}/${id}`)
    .pipe(map(v => true));
  }
}
