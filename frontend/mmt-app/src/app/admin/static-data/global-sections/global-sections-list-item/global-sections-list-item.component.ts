import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { GlobalSection } from '../../models/global-section.model';

@Component({
  selector: 'mmt-global-sections-list-item',
  templateUrl: './global-sections-list-item.component.html',
  styleUrls: [
    './global-sections-list-item.component.scss',
    '../../../../list-item-details.scss'
  ]
})
export class GlobalSectionsListItemComponent {
  @Input() section?: GlobalSection;
  @Output() editSectionEvent = new EventEmitter<GlobalSection>();
  @Output() removeSectionEvent = new EventEmitter<GlobalSection>();

  constructor(private router:Router)
  {

  }

  detailsClick()
  {
    if (this.section)
      this.router.navigate(['/admin/static-data/global-sections', this.section.id])
  }

  deleteClick()
  {
    if (this.section && confirm(`Are you sure to delete ${this.section.name}?`))
      this.removeSectionEvent.emit(this.section);
  }
}
