import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { LocalSection } from '../models/local-section.model';

@Component({
  selector: 'mmt-local-sections-list',
  templateUrl: './local-sections-list.component.html',
  styleUrls: ['./local-sections-list.component.scss']
})
export class LocalSectionsListComponent {
  @Input() sections!: LocalSection[];
  @Output() createSectionItemEvent = new EventEmitter<LocalSection>();
  @Output() removeSectionItemEvent = new EventEmitter<LocalSection>();

  submitLocalSection(addData: ItemWithDescription)
  {
    this.createSectionItemEvent.emit({ ...addData })
  }

  removeLocalSection(removeData: ItemWithDescription)
  {
    this.removeSectionItemEvent.emit({ ...removeData })
  }
}
