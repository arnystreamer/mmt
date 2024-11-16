import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Receipt } from '../models/receipt.model';
import { ItemGuid } from 'src/app/models/item-guid';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'mmt-receipt-list-item',
  templateUrl: './receipt-list-item.component.html',
  styleUrls: [
    './receipt-list-item.component.scss',
    '../../list-item-details.scss'
  ]
})
export class ReceiptListItemComponent {
  @Input() item!: Receipt;
  @Output() removeEvent = new EventEmitter<ItemGuid>();

  constructor(private router: Router, private route: ActivatedRoute)
  {

  }

  detailsClick()
  {
    if (!this.item)
      return;

    this.router.navigate([this.item.id], { relativeTo: this.route });
  }

  deleteClick()
  {
    if (!this.item || !confirm(`Are you sure to delete receipt ${this.item.id}?`))
      return;

    this.removeEvent.emit(this.item);
  }
}
