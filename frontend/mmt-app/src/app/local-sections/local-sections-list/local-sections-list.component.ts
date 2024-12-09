import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';
import { ItemIdentity } from 'src/app/models/item-identity';
import { SectionEdit } from 'src/app/models/static-data/section-edit.model';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
    selector: 'mmt-local-sections-list',
    templateUrl: './local-sections-list.component.html',
    styleUrls: ['./local-sections-list.component.scss'],
    standalone: false
})
export class LocalSectionsListComponent {
  @Input() sections!: Section[];
  @Output() createSectionItemEvent = new EventEmitter<SectionEdit>();
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
