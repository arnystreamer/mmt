import { ResolveFn } from '@angular/router';
import { Receipt } from '../models/receipt.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { ReceiptsService } from './receipts.service';
import { inject } from '@angular/core';
import { ReceiptRequestParams } from '../models/receipt-request-params.model';

export const receiptsResolver: ResolveFn<CollectionApi<Receipt>> = (route, state) => {
  const receiptsService = inject(ReceiptsService);

  const receiptRequestParamsJson = localStorage.getItem('ReceiptRequestParams');
  const receiptRequestParams: ReceiptRequestParams | undefined = receiptRequestParamsJson ? JSON.parse(receiptRequestParamsJson) : undefined;

  return receiptsService.getAll(receiptRequestParams || {}, 0, 10000);
};
