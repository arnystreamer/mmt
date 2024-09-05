import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-global-sections-list-item',
  templateUrl: './global-sections-list-item.component.html',
  styleUrls: ['./global-sections-list-item.component.scss']
})
export class GlobalSectionsListItemComponent {
  @Input() section!: ItemWithDescription;
  @Output() editSectionEvent = new EventEmitter<ItemWithDescription>();
  @Output() removeSectionEvent = new EventEmitter<ItemWithDescription>();

  deleteClick()
  {
    this.removeSectionEvent.emit(this.section);
  }
}
