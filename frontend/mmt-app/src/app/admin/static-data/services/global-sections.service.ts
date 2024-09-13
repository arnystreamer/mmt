import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GlobalSectionsService {

  private apiUrl = environment.apiUrl;
  private globalSectionsUrl = `${this.apiUrl}/global-sections`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<ItemWithDescription>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<ItemWithDescription>>(this.globalSectionsUrl, { params: params });
  }

  get(id: number) : Observable<ItemWithDescription>
  {
    return this.httpClient.get<ItemWithDescription>(`${this.globalSectionsUrl}/${id}`);
  }

  post(item: ItemWithDescription) : Observable<ItemWithDescription>
  {
    return this.httpClient.post<ItemWithDescription>(this.globalSectionsUrl, item);
  }

  put(item: ItemWithDescription) : Observable<ItemWithDescription>
  {
    return this.httpClient.put<ItemWithDescription>(this.globalSectionsUrl, item);
  }

  delete(id: number) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.globalSectionsUrl}/${id}`)
    .pipe(map(v => true));
  }
}
