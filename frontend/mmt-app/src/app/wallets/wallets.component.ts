import { Component, OnInit } from '@angular/core';
import { WalletsService } from './services/wallets.service';
import { ActivatedRoute } from '@angular/router';
import { ItemIdentity } from '../models/item-identity';
import { Wallet } from '../models/static-data/wallet.model';
import { WalletEdit } from '../models/static-data/wallet-edit.model';

@Component({
    selector: 'mmt-wallets',
    templateUrl: './wallets.component.html',
    styleUrls: ['./wallets.component.scss'],
    standalone: false
})
export class WalletsComponent implements OnInit {

  public items: Wallet[] = [];

  constructor(private route: ActivatedRoute,
    private walletsService : WalletsService) {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  createWallet(item: WalletEdit)
  {
    this.walletsService.post(item).subscribe({next: v => this.items.push(v) });
  }

  removeWallet(item: ItemIdentity)
  {
    this.walletsService.delete(item.id).subscribe({ next: v => {
        if (v === false)
          return;

        const index = this.items.findIndex(i => i.id === item.id);
        this.items.splice(index, 1);
      }
    });
  }

}
