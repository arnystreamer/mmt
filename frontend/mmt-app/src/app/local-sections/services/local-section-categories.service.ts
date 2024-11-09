import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { SectionCategory } from 'src/app/models/sections/section-category.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocalSectionCategoriesService {
  private apiUrl = environment.apiUrl;
  private localSectionCategoryUrlResolver = (sectionId: number) => `${this.apiUrl}/local-sections/${sectionId}/categories`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(sectionId: number, skip?: number, take?: number): Observable<CollectionApi<SectionCategory>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<SectionCategory>>(this.localSectionCategoryUrlResolver(sectionId), { params: params });
  }

  get(sectionId: number, id: number) : Observable<SectionCategory>
  {
    return this.httpClient.get<SectionCategory>(`${this.localSectionCategoryUrlResolver(sectionId)}/${id}`);
  }

  post(sectionId: number, item: SectionCategory) : Observable<SectionCategory>
  {
    return this.httpClient.post<SectionCategory>(this.localSectionCategoryUrlResolver(sectionId), item);
  }

  put(sectionId: number, item: SectionCategory) : Observable<SectionCategory>
  {
    return this.httpClient.put<SectionCategory>(this.localSectionCategoryUrlResolver(sectionId), item);
  }

  delete(sectionId: number, id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.localSectionCategoryUrlResolver(sectionId)}/${id}`)
      .pipe(map(v => true));
  }
}
