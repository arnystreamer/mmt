import { Injectable } from '@angular/core';
import { CollectionApi } from 'src/app/models/collection-api';
import { map, Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { SectionCategoryEdit } from 'src/app/models/sections/section-category-edit.model';
import { SectionCategory } from 'src/app/models/sections/section-category.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalCategoriesService {

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

  post(sectionId: number, item: SectionCategoryEdit) : Observable<SectionCategory>
  {
    return this.httpClient.post<SectionCategory>(this.globalSectionsUrlResolver(sectionId), item);
  }

  delete(sectionId: number, id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.globalSectionsUrlResolver(sectionId)}/${id}`)
    .pipe(map(v => true));
  }
}
