import { Component } from '@angular/core';
import { Wallet } from '../models/wallet.model';
import { ActivatedRoute } from '@angular/router';
import { WalletSection } from '../wallet-section.model';
import { WalletSectionsService } from '../services/wallet-sections.service';

@Component({
  selector: 'mmt-wallet-details',
  templateUrl: './wallet-details.component.html',
  styleUrls: [
    './wallet-details.component.scss',
    '../../list-item-details.scss'
  ]
})
export class WalletDetailsComponent {
  public wallet?: Wallet;
  public sections: WalletSection[] = [];

  constructor(private route: ActivatedRoute, private walletSectionsService: WalletSectionsService)
  {

  }

  ngOnInit(): void {
     this.route.data.subscribe(({item}) => this.wallet = item);
     this.route.data.subscribe(({ itemsApi }) => this.sections.push(...itemsApi.items));
  }

  createWalletSection(item: WalletSection)
  {
    if (!this.wallet || !item)
      return;

    this.walletSectionsService.post(this.wallet.id, item).subscribe({next: v => this.sections.push(v) });
  }

  removeWalletSection(item: WalletSection)
  {
    if (!this.wallet || !item)
      return;

    this.walletSectionsService.delete(this.wallet.id, item.id).subscribe({next: v => {
      if (v === false)
        return;

      const index = this.sections.findIndex(i => i.id === item.id);
      this.sections.splice(index, 1);
    }})
  }
}
