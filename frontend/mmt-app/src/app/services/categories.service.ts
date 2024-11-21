import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SectionCategory } from '../models/sections/section-category.model';
import { CollectionApi } from '../models/collection-api';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  private apiUrl = environment.apiUrl;
  private globalSectionsUrlResolver = (sectionId: number) => `${this.apiUrl}/global-sections/${sectionId}/categories`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(sectionId: number, skip?: number, take?: number): Observable<CollectionApi<SectionCategory>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<SectionCategory>>(this.globalSectionsUrlResolver(sectionId), { params: params });
  }

  get(sectionId: number, id: number) : Observable<SectionCategory>
  {
    return this.httpClient.get<SectionCategory>(`${this.globalSectionsUrlResolver(sectionId)}/${id}`);
  }
}
