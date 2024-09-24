import { Component, Input } from '@angular/core';
import { GlobalSectionCategory } from '../../models/global-section-category.model';

@Component({
  selector: 'mmt-gsection-categories-list-item',
  templateUrl: './gsection-categories-list-item.component.html',
  styleUrls: ['./gsection-categories-list-item.component.scss']
})
export class GsectionCategoriesListItemComponent {
  @Input() category?: GlobalSectionCategory;
}
