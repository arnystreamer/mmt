import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GlobalSection } from '../../models/global-section.model';

@Component({
  selector: 'mmt-global-sections-list',
  templateUrl: './global-sections-list.component.html',
  styleUrls: ['./global-sections-list.component.scss']
})
export class GlobalSectionsListComponent {
  @Input() sections!: GlobalSection[];
  @Output() removeSectionEvent = new EventEmitter<GlobalSection>();
}
