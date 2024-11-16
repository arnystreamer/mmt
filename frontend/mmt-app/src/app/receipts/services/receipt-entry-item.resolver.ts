import { ResolveFn } from '@angular/router';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { ReceiptEntriesService } from './receipt-entries.service';
import { inject } from '@angular/core';

export const receiptEntryItemResolver: ResolveFn<ReceiptEntry | undefined> = (route, state) => {
  const receiptEntriesService = inject(ReceiptEntriesService);

  var receiptId = route.paramMap.get('id');
  if (!receiptId)
  {
    return undefined;
  }

  var receiptEntryId = route.paramMap.get('entryId');
  if (!receiptEntryId)
  {
    return undefined;
  }

  return receiptEntriesService.get(receiptId, receiptEntryId);
};
