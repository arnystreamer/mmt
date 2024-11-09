import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { SectionCategory } from 'src/app/models/sections/section-category.model';

@Component({
  selector: 'mmt-local-section-categories-list',
  templateUrl: './local-section-categories-list.component.html',
  styleUrls: ['./local-section-categories-list.component.scss']
})
export class LocalSectionCategoriesListComponent {
  @Input() categories!: SectionCategory[];
  @Output() createCategoryItemEvent = new EventEmitter<SectionCategory>();
  @Output() removeCategoryItemEvent = new EventEmitter<SectionCategory>();

  submitLocalSectionCategory(addData: ItemWithDescription)
  {
    this.createCategoryItemEvent.emit({ sectionId: undefined, ...addData })
  }

  removeLocalSectionCategory(removeData: ItemWithDescription)
  {
    this.removeCategoryItemEvent.emit({ sectionId: undefined, ...removeData })
  }
}
