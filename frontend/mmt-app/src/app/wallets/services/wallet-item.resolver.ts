import { ResolveFn } from '@angular/router';
import { inject } from '@angular/core';
import { WalletsService } from './wallets.service';
import { Wallet } from 'src/app/models/static-data/wallet.model';

export const walletItemResolver: ResolveFn<Wallet | undefined> = (route, state) => {
  const walletsService = inject(WalletsService);

  var walletId = route.paramMap.get('id');
  if (!walletId)
  {
    return undefined;
  }

  return walletsService.get(parseInt(walletId));
};
