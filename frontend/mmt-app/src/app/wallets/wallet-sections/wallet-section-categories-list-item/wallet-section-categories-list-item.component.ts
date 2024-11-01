import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSectionCategory } from '../../wallet-section-category.model';
import { Router } from '@angular/router';

@Component({
  selector: 'mmt-wallet-section-categories-list-item',
  templateUrl: './wallet-section-categories-list-item.component.html',
  styleUrls: ['./wallet-section-categories-list-item.component.scss', '../../../list-item-details.scss']
})
export class WalletSectionCategoriesListItemComponent {
  @Input() category?: WalletSectionCategory;
  @Output() removeEvent = new EventEmitter<WalletSectionCategory>();

  constructor(private router: Router)
  {

  }

  deleteClick()
  {
    if (!this.category || !confirm(`Are you sure to delete ${this.category.name}?`))
      return;

    this.removeEvent.emit(this.category);
  }
}
