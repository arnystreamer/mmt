import { ResolveFn } from '@angular/router';
import { Receipt } from '../models/receipt.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { ReceiptsService } from './receipts.service';
import { inject } from '@angular/core';

export const receiptsResolver: ResolveFn<CollectionApi<Receipt>> = (route, state) => {
  const receiptsService = inject(ReceiptsService);

  return receiptsService.getAll();
};
