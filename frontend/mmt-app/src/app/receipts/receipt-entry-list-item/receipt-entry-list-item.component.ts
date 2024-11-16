import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { ItemGuid } from 'src/app/models/item-guid';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'mmt-receipt-entry-list-item',
  templateUrl: './receipt-entry-list-item.component.html',
  styleUrls: [
    './receipt-entry-list-item.component.scss',
    '../../list-item-details.scss'
  ]
})
export class ReceiptEntryListItemComponent {
  @Input() item?: ReceiptEntry;
  @Output() removeEvent = new EventEmitter<ItemGuid>();

  constructor(private router: Router, private route: ActivatedRoute)
  {

  }

  detailsClick()
  {
    if (!this.item)
      return;

    this.router.navigate(['entries/',this.item.id], { relativeTo: this.route });
  }

  deleteClick()
  {
    if (!this.item || !confirm(`Are you sure to delete receipt entry ${this.item.id}?`))
      return;

    this.removeEvent.emit(this.item);
  }
}
