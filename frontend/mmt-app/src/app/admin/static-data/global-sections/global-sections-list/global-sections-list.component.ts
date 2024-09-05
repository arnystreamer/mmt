import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-global-sections-list',
  templateUrl: './global-sections-list.component.html',
  styleUrls: ['./global-sections-list.component.scss']
})
export class GlobalSectionsListComponent {
  @Input() sections!: ItemWithDescription[];
  @Output() addSectionEvent = new EventEmitter<ItemWithDescription>();
  @Output() editSectionEvent = new EventEmitter<ItemWithDescription>();
  @Output() removeSectionEvent = new EventEmitter<ItemWithDescription>();
}
