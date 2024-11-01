import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSectionCategory } from '../../wallet-section-category.model';

@Component({
  selector: 'mmt-wallet-section-categories-list',
  templateUrl: './wallet-section-categories-list.component.html',
  styleUrls: ['./wallet-section-categories-list.component.scss']
})
export class WalletSectionCategoriesListComponent {
  @Input() categories!: WalletSectionCategory[];
  @Output() createCategoryItemEvent = new EventEmitter<WalletSectionCategory>();
  @Output() removeCategoryItemEvent = new EventEmitter<WalletSectionCategory>();
}
