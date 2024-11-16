import { ResolveFn } from '@angular/router';
import { Receipt } from '../models/receipt.model';
import { inject } from '@angular/core';
import { ReceiptsService } from './receipts.service';

export const receiptItemResolver: ResolveFn<Receipt | undefined> = (route, state) => {
  const receiptService = inject(ReceiptsService);

  var receiptId = route.paramMap.get('id');
  if (!receiptId)
  {
    return undefined;
  }

  return receiptService.get(receiptId);
};
