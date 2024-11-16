import { Component } from '@angular/core';
import { WalletSection } from '../../models/wallet-section.model';
import { ActivatedRoute } from '@angular/router';
import { WalletSectionCategoriesService } from '../../services/wallet-section-categories.service';
import { SectionCategory } from '../../../models/sections/section-category.model';
import { SectionCategoryEdit } from 'src/app/models/sections/section-category-edit.model';
import { ItemIdentity } from 'src/app/models/item-identity';

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
  public categories: SectionCategory[] = [];

  constructor(private route: ActivatedRoute, private walletSectionCategoriesService: WalletSectionCategoriesService)
  {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ parent }) => this.wallet = parent);
    this.route.data.subscribe(({ item }) => this.section = item);
    this.route.data.subscribe(({ itemsApi }) => this.categories.push(...itemsApi.items));
  }

  createWalletSectionCategory(item: SectionCategoryEdit)
  {
    if (!this.wallet || !this.section || !item)
      return;

    this.walletSectionCategoriesService.post(this.wallet.id, this.section.id, item).subscribe({next: v => this.categories.push(v) });
  }

  removeWalletSectionCategory(item: ItemIdentity)
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
