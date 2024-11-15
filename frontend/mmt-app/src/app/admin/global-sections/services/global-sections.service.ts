import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { environment } from 'src/environments/environment';
import { GlobalSection } from '../models/global-section.model';

@Injectable({
  providedIn: 'root'
})
export class GlobalSectionsService {

  private apiUrl = environment.apiUrl;
  private globalSectionsUrl = `${this.apiUrl}/global-sections`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<GlobalSection>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<GlobalSection>>(this.globalSectionsUrl, { params: params });
  }

  get(id: number) : Observable<GlobalSection>
  {
    return this.httpClient.get<GlobalSection>(`${this.globalSectionsUrl}/${id}`);
  }

  post(item: GlobalSection) : Observable<GlobalSection>
  {
    return this.httpClient.post<GlobalSection>(this.globalSectionsUrl, item);
  }

  put(item: GlobalSection) : Observable<GlobalSection>
  {
    return this.httpClient.put<GlobalSection>(this.globalSectionsUrl, item);
  }

  delete(id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.globalSectionsUrl}/${id}`)
    .pipe(map(v => true));
  }
}
