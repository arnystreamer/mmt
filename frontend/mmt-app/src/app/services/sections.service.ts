import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Section } from '../models/static-data/section.model';
import { CollectionApi } from '../models/collection-api';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SectionsService {

  private apiUrl = environment.apiUrl;
  private globalSectionsUrl = `${this.apiUrl}/global-sections`;
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

    return this.httpClient.get<CollectionApi<Section>>(this.globalSectionsUrl, { params: params });
  }

  get(id: number) : Observable<Section>
  {
    return this.httpClient.get<Section>(`${this.globalSectionsUrl}/${id}`);
  }
}
