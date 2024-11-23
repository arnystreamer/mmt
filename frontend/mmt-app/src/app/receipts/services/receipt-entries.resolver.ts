import { ResolveFn } from '@angular/router';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { ReceiptEntriesService } from './receipt-entries.service';
import { inject } from '@angular/core';

export const receiptEntriesResolver: ResolveFn<CollectionApi<ReceiptEntry> | undefined> = (route, state) => {
  const receiptEntriesService = inject(ReceiptEntriesService);

  var receiptId = route.paramMap.get('id');
  if (!receiptId)
  {
    return undefined;
  }

  return receiptEntriesService.getAll(receiptId, 0, 100);
};
