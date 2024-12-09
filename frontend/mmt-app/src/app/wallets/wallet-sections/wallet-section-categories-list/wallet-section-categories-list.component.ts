import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SectionCategory } from '../../../models/sections/section-category.model';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';
import { SectionCategoryEdit } from 'src/app/models/sections/section-category-edit.model';
import { ItemIdentity } from 'src/app/models/item-identity';

@Component({
    selector: 'mmt-wallet-section-categories-list',
    templateUrl: './wallet-section-categories-list.component.html',
    styleUrls: ['./wallet-section-categories-list.component.scss'],
    standalone: false
})
export class WalletSectionCategoriesListComponent {
  @Input() categories!: SectionCategory[];
  @Output() createCategoryItemEvent = new EventEmitter<SectionCategoryEdit>();
  @Output() removeCategoryItemEvent = new EventEmitter<ItemIdentity>();

  submitWalletSectionCategories(addData: ItemWithDescriptionEdit)
  {
    this.createCategoryItemEvent.emit({ ...addData })
  }

  removeWalletSectionCategories(removeData: ItemWithDescription)
  {
    this.removeCategoryItemEvent.emit({ ...removeData })
  }
}
