import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { ProductEdit } from 'src/app/models/static-data/product-edit.model';
import { Product } from 'src/app/models/static-data/product.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private apiUrl = environment.apiUrl;
  private productsUrl = `${this.apiUrl}/products`;

  constructor(
    private httpClient: HttpClient) { }

  getAll(skip?: number, take?: number): Observable<CollectionApi<Product>>
  {
    let params = new HttpParams();
    if (skip)
      params = params.set("skip", skip);

    if (take)
      params = params.set("take", take);

    return this.httpClient.get<CollectionApi<Product>>(this.productsUrl, { params: params });
  }

  get(id: string) : Observable<Product>
  {
    return this.httpClient.get<Product>(`${this.productsUrl}/${id}`);
  }

  post(item: ProductEdit) : Observable<Product>
  {
    item.createTime = new Date();
    return this.httpClient.post<Product>(this.productsUrl, item);
  }

  delete(id: string) : Observable<boolean>
  {
    return this.httpClient.delete(`${this.productsUrl}/${id}`)
      .pipe(map(v => true));
  }
}
