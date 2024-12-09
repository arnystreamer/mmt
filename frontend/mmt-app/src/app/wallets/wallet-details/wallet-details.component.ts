import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WalletSection } from '../models/wallet-section.model';
import { WalletSectionsService } from '../services/wallet-sections.service';
import { ItemIdentity } from 'src/app/models/item-identity';
import { Wallet } from 'src/app/models/static-data/wallet.model';
import { SectionEdit } from 'src/app/models/static-data/section-edit.model';

@Component({
    selector: 'mmt-wallet-details',
    templateUrl: './wallet-details.component.html',
    styleUrls: [
        './wallet-details.component.scss',
        '../../list-item-details.scss'
    ],
    standalone: false
})
export class WalletDetailsComponent implements OnInit {
  public wallet?: Wallet;
  public sections: WalletSection[] = [];

  constructor(private route: ActivatedRoute, private walletSectionsService: WalletSectionsService)
  {

  }

  ngOnInit(): void {
     this.route.data.subscribe(({item}) => this.wallet = item);
     this.route.data.subscribe(({ itemsApi }) => this.sections.push(...itemsApi.items));
  }

  createWalletSection(item: SectionEdit)
  {
    if (!this.wallet || !item)
      return;

    this.walletSectionsService.post(this.wallet.id, item).subscribe({next: v => this.sections.push(v) });
  }

  removeWalletSection(item: ItemIdentity)
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
