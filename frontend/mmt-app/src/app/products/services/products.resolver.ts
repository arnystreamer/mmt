import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { Product } from 'src/app/models/static-data/product.model';
import { ProductsService } from './products.service';
import { inject } from '@angular/core';

export const productsResolver: ResolveFn<CollectionApi<Product> | undefined> = (route, state) => {
  const productsService = inject(ProductsService);

  return productsService.getAll(0, 1000);
};
