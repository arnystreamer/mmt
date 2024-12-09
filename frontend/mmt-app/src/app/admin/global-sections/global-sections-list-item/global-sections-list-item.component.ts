import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ItemIdentity } from 'src/app/models/item-identity';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
    selector: 'mmt-global-sections-list-item',
    templateUrl: './global-sections-list-item.component.html',
    styleUrls: [
        './global-sections-list-item.component.scss',
        '../../../list-item-details.scss'
    ],
    standalone: false
})
export class GlobalSectionsListItemComponent {
  @Input() section?: Section;
  @Output() removeSectionEvent = new EventEmitter<ItemIdentity>();

  constructor(private router:Router)
  {

  }

  detailsClick()
  {
    if (this.section)
      this.router.navigate(['/admin/global-sections', this.section.id])
  }

  deleteClick()
  {
    if (this.section && confirm(`Are you sure to delete ${this.section.name}?`))
      this.removeSectionEvent.emit(this.section);
  }
}
