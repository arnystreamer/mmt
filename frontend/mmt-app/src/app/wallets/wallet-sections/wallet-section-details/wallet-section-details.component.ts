import { Component } from '@angular/core';
import { WalletSection } from '../../wallet-section.model';
import { ActivatedRoute } from '@angular/router';
import { WalletSectionCategoriesService } from '../../services/wallet-section-categories.service';
import { WalletSectionCategory } from '../../wallet-section-category.model';

@Component({
  selector: 'mmt-wallet-section-details',
  templateUrl: './wallet-section-details.component.html',
  styleUrls: [
    './wallet-section-details.component.scss',
    '../../../list-item-details.scss'
  ]
})
export class WalletSectionDetailsComponent {
  public wallet?: WalletSection;
  public section?: WalletSection;
  public categories: WalletSectionCategory[] = [];

  constructor(private route: ActivatedRoute, private walletSectionCategoriesService: WalletSectionCategoriesService)
  {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ parent }) => this.wallet = parent);
    this.route.data.subscribe(({ item }) => this.section = item);
    this.route.data.subscribe(({ itemsApi }) => this.categories.push(...itemsApi.items));
  }

  createWalletSectionCategory(item: WalletSectionCategory)
  {
    if (!this.wallet || !this.section || !item)
      return;

    this.walletSectionCategoriesService.post(this.wallet.id, this.section.id, item).subscribe({next: v => this.categories.push(v) });
  }

  removeWalletSectionCategory(item: WalletSectionCategory)
  {
    if (!this.wallet || !this.section || !item)
      return;

    this.walletSectionCategoriesService.delete(this.wallet.id, this.section.id, item.id).subscribe({next: v => {
      if (v === false)
        return;

      const index = this.categories.findIndex(i => i.id === item.id);
      this.categories.splice(index, 1);
    }})
  }
}
