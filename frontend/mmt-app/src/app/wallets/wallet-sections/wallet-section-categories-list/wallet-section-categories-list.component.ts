import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SectionCategory } from '../../../models/sections/section-category.model';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-wallet-section-categories-list',
  templateUrl: './wallet-section-categories-list.component.html',
  styleUrls: ['./wallet-section-categories-list.component.scss']
})
export class WalletSectionCategoriesListComponent {
  @Input() categories!: SectionCategory[];
  @Output() createCategoryItemEvent = new EventEmitter<SectionCategory>();
  @Output() removeCategoryItemEvent = new EventEmitter<SectionCategory>();

  submitWalletSectionCategories(addData: ItemWithDescription)
  {
    this.createCategoryItemEvent.emit({ sectionId: undefined, ...addData })
  }

  removeWalletSectionCategories(removeData: ItemWithDescription)
  {
    this.removeCategoryItemEvent.emit({ sectionId: undefined, ...removeData })
  }
}
