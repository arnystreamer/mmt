import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
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

  constructor(private router:Router)
  {

  }

  detailsClick()
  {
    this.router.navigate(['/admin/static-data/global-sections', this.section.id])
  }

  deleteClick()
  {
    this.removeSectionEvent.emit(this.section);
  }
}
