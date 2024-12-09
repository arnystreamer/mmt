import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemIdentity } from 'src/app/models/item-identity';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';
import { SectionCategoryEdit } from 'src/app/models/sections/section-category-edit.model';
import { SectionCategory } from 'src/app/models/sections/section-category.model';

@Component({
    selector: 'mmt-local-section-categories-list',
    templateUrl: './local-section-categories-list.component.html',
    styleUrls: ['./local-section-categories-list.component.scss'],
    standalone: false
})
export class LocalSectionCategoriesListComponent {
  @Input() categories!: SectionCategory[];
  @Output() createCategoryItemEvent = new EventEmitter<SectionCategoryEdit>();
  @Output() removeCategoryItemEvent = new EventEmitter<ItemIdentity>();

  submitLocalSectionCategory(addData: ItemWithDescriptionEdit)
  {
    this.createCategoryItemEvent.emit({ ...addData })
  }

  removeLocalSectionCategory(removeData: ItemIdentity)
  {
    this.removeCategoryItemEvent.emit({ ...removeData })
  }
}
