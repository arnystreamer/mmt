import { ResolveFn } from '@angular/router';
import { CollectionApi } from 'src/app/models/collection-api';
import { WalletsService } from './wallets.service';
import { inject } from '@angular/core';
import { Wallet } from 'src/app/models/static-data/wallet.model';

export const walletsResolver: ResolveFn<CollectionApi<Wallet>> = (route, state) => {
  const walletsService = inject(WalletsService);

  return walletsService.getAll();
};
