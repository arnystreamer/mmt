import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  #loadersCount = 0;

  constructor() { }

  showLoader()
  {
    setTimeout(() => this.#loadersCount++, 300);
  }

  hideLoader()
  {
    this.#loadersCount--;
  }

  get isShowLoader()
  {
    return this.#loadersCount > 0;
  }
}
