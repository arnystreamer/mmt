import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { ItemGuid } from 'src/app/models/item-guid';
import { ReceiptEntryEdit } from '../models/receipt-entry-edit.model';
import { Receipt } from '../models/receipt.model';

@Component({
  selector: 'mmt-receipt-entries-list',
  templateUrl: './receipt-entries-list.component.html',
  styleUrls: ['./receipt-entries-list.component.scss']
})
export class ReceiptEntriesListComponent {
  @Input() parentReceipt?: Receipt;
  @Input() items!: ReceiptEntry[];
  @Output() createReceiptEntryItemEvent = new EventEmitter<ReceiptEntryEdit>();
  @Output() removeReceiptEntryItemEvent = new EventEmitter<ItemGuid>();

  submitReceiptEntry(addData: ReceiptEntryEdit)
  {
    this.createReceiptEntryItemEvent.emit({ ...addData })
  }

  removeReceiptEntry(removeData: ItemGuid)
  {
    this.removeReceiptEntryItemEvent.emit({ ...removeData })
  }
}
