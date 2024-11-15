import { Component, OnInit } from '@angular/core';
import { Wallet } from './models/wallet.model';
import { WalletsService } from './services/wallets.service';
import { ActivatedRoute } from '@angular/router';
import { WalletEdit } from './models/wallet-edit.model';
import { ItemIdentity } from '../models/item-identity';

@Component({
  selector: 'mmt-wallets',
  templateUrl: './wallets.component.html',
  styleUrls: ['./wallets.component.scss']
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
