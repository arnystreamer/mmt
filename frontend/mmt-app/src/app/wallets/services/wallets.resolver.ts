import { ResolveFn } from '@angular/router';
import { Wallet } from '../models/wallet.model';
import { CollectionApi } from 'src/app/models/collection-api';
import { WalletsService } from './wallets.service';
import { inject } from '@angular/core';

export const walletsResolver: ResolveFn<CollectionApi<Wallet>> = (route, state) => {
  const walletsService = inject(WalletsService);

  return walletsService.getAll();
};
