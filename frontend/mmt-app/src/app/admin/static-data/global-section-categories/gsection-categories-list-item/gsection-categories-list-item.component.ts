import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GlobalSectionCategory } from '../../models/global-section-category.model';

@Component({
  selector: 'mmt-gsection-categories-list-item',
  templateUrl: './gsection-categories-list-item.component.html',
  styleUrls: [
    './gsection-categories-list-item.component.scss',
    '../../../../list-item-details.scss'
  ]
})
export class GsectionCategoriesListItemComponent {
  @Input() category?: GlobalSectionCategory;

  @Output() removeCategoryEvent = new EventEmitter<GlobalSectionCategory>();

  deleteClick()
  {
    if (this.category && confirm(`Are you sure to delete ${this.category.name}?`))
      this.removeCategoryEvent.emit(this.category);
  }
}
