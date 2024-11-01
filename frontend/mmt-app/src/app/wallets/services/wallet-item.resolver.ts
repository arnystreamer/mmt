import { ResolveFn } from '@angular/router';
import { Wallet } from '../models/wallet.model';
import { inject } from '@angular/core';
import { WalletsService } from './wallets.service';

export const walletItemResolver: ResolveFn<Wallet | undefined> = (route, state) => {
  const walletsService = inject(WalletsService);

  var walletId = route.paramMap.get('id');
  if (!walletId)
  {
    return undefined;
  }

  return walletsService.get(parseInt(walletId));
};
