import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { CollectionApi } from 'src/app/models/collection-api';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Injectable({
  providedIn: 'root'
})
export class GlobalSectionsService {

  private items: Array<ItemWithDescription> = [
    {
      id: 1,
      name: "Some name",
      description: "Desc"
    },
    {
      id: 2,
      name: "Other name",
      description: "Desc 2"
    },
    {
      id: 3,
      name: "Different name"
    },
  ];

  constructor() { }

  getAll(skip: number | null, take: number | null): Observable<CollectionApi<ItemWithDescription>>
  {
    return of({ total: this.items.length, skip: 0, take: this.items.length, count: this.items.length, items: this.items });
  }

  get(id: number) : Observable<ItemWithDescription>
  {
    return of(this.items.find(i => i.id == id) || {id: 0, name: '', description: ''});
  }

  post(item: ItemWithDescription) : Observable<ItemWithDescription>
  {
    const currentId = Math.max(...this.items.map(v => v.id)) + 1;

    item.id = currentId;
    this.items.push(item);

    return of(item);
  }

  put(item: ItemWithDescription) : Observable<ItemWithDescription>
  {
    let index = this.items.findIndex(i => i.id == item.id)
    if (index === -1)
      throw `Item not found with ID = ${item.id}`;

    this.items[index].name = item.name;
    this.items[index].description = item.description;

    return of(item);
  }

  delete(id: number) : Observable<boolean>
  {
    let index = this.items.findIndex(i => i.id == id)
    if (index === -1)
      return of(false);

    this.items.splice(index, 1);

    return of(true);
  }
}
