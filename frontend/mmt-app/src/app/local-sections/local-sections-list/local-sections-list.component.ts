import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { LocalSection } from '../models/local-section.model';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';
import { ItemIdentity } from 'src/app/models/item-identity';
import { LocalSectionEdit } from '../models/local-section-edit.model';

@Component({
  selector: 'mmt-local-sections-list',
  templateUrl: './local-sections-list.component.html',
  styleUrls: ['./local-sections-list.component.scss']
})
export class LocalSectionsListComponent {
  @Input() sections!: LocalSection[];
  @Output() createSectionItemEvent = new EventEmitter<LocalSectionEdit>();
  @Output() removeSectionItemEvent = new EventEmitter<ItemIdentity>();

  submitLocalSection(addData: ItemWithDescriptionEdit)
  {
    this.createSectionItemEvent.emit({ ...addData })
  }

  removeLocalSection(removeData: ItemIdentity)
  {
    this.removeSectionItemEvent.emit({ ...removeData })
  }
}
