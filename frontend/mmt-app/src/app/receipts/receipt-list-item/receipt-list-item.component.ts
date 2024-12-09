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
    ],
    standalone: false
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

  getItemSum(receipt: Receipt) : number
  {
    return receipt.entries.map(e => e.quantity * e.price).reduce((sum, currency) => sum + currency, 0.0);
  }

  getMajorSections(receipt: Receipt): string
  {
    var sectionsByAmount: any = [];

    for(var i = 0; i < receipt.entries.length; i++)
    {
      const entry = receipt.entries[i];
      const key = entry.product.section.name;
      const amount = entry.price * entry.quantity;
      if (sectionsByAmount[key])
      {
        sectionsByAmount[key] += amount;
      }
      else
      {
        sectionsByAmount[key] = amount;
      }
    }

    const result = Object.keys(sectionsByAmount)
      .map(k => [k, sectionsByAmount[k]]);

    result.sort((a, b) => b[1] - a[1]);

    return result.map(k => k[0]).slice(0, 2).join(', ') + (result.length > 2 ? ' , etc.': '');
  }
}
