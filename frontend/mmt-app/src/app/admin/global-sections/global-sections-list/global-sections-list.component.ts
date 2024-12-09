import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemIdentity } from 'src/app/models/item-identity';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
    selector: 'mmt-global-sections-list',
    templateUrl: './global-sections-list.component.html',
    styleUrls: ['./global-sections-list.component.scss'],
    standalone: false
})
export class GlobalSectionsListComponent {
  @Input() sections!: Section[];
  @Output() removeSectionEvent = new EventEmitter<ItemIdentity>();
}
