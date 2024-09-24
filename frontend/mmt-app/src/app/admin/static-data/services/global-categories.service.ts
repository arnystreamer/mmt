import { Injectable } from '@angular/core';
import { GlobalSectionCategory } from '../models/global-section-category.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { map, Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GlobalCategoriesService {

  private apiUrl = environment.apiUrl;
  private globalSectionsUrlResolver = (sectionId: number) => `${this.apiUrl}/global-sections/${sectionId}/categories`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(sectionId: number, skip?: number, take?: number): Observable<CollectionApi<GlobalSectionCategory>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<GlobalSectionCategory>>(this.globalSectionsUrlResolver(sectionId), { params: params });
  }

  get(sectionId: number, id: number) : Observable<GlobalSectionCategory>
  {
    return this.httpClient.get<GlobalSectionCategory>(`${this.globalSectionsUrlResolver(sectionId)}/${id}`);
  }

  post(sectionId: number, item: GlobalSectionCategory) : Observable<GlobalSectionCategory>
  {
    return this.httpClient.post<GlobalSectionCategory>(this.globalSectionsUrlResolver(sectionId), item);
  }

  put(sectionId: number, item: GlobalSectionCategory) : Observable<GlobalSectionCategory>
  {
    return this.httpClient.put<GlobalSectionCategory>(this.globalSectionsUrlResolver(sectionId), item);
  }

  delete(sectionId: number, id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.globalSectionsUrlResolver(sectionId)}/${id}`)
    .pipe(map(v => true));
  }
}
