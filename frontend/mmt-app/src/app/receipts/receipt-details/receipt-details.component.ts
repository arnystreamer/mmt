import { Component, OnInit } from '@angular/core';
import { Receipt } from '../models/receipt.model';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { ReceiptEntryEdit } from '../models/receipt-entry-edit.model';
import { ItemGuid } from 'src/app/models/item-guid';
import { ActivatedRoute } from '@angular/router';
import { ReceiptEntriesService } from '../services/receipt-entries.service';

@Component({
    selector: 'mmt-receipt-details',
    templateUrl: './receipt-details.component.html',
    styleUrls: [
        './receipt-details.component.scss',
        '../../list-item-details.scss'
    ],
    standalone: false
})
export class ReceiptDetailsComponent implements OnInit {
  public receipt?: Receipt;
  public receiptEntries: ReceiptEntry[] = [];

  constructor(private route: ActivatedRoute, private receiptEntriesService: ReceiptEntriesService)
  {

  }

  ngOnInit(): void {
     this.route.data.subscribe(({ item }) => this.receipt = item);
     this.route.data.subscribe(({ itemsApi }) => this.receiptEntries.push(...itemsApi.items));
  }

  createReceiptEntries(item: ReceiptEntryEdit)
  {
    if (!this.receipt || !item)
      return;

    this.receiptEntriesService.post(this.receipt.id, item).subscribe({next: v => this.receiptEntries.push(v) });
  }

  removeReceiptEntries(item: ItemGuid)
  {
    if (!this.receipt || !item)
      return;

    this.receiptEntriesService.delete(this.receipt.id, item.id).subscribe({next: v => {
      if (v === false)
        return;

      const index = this.receiptEntries.findIndex(i => i.id === item.id);
      this.receiptEntries.splice(index, 1);
    }})
  }
}
