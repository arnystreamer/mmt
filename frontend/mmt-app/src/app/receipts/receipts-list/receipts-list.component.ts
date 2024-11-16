import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Receipt } from '../models/receipt.model';
import { ReceiptEdit } from '../models/receipt-edit.model';
import { ItemGuid } from 'src/app/models/item-guid';

@Component({
  selector: 'mmt-receipts-list',
  templateUrl: './receipts-list.component.html',
  styleUrls: ['./receipts-list.component.scss']
})
export class ReceiptsListComponent {
  @Input() receipts!: Receipt[];
  @Output() removeReceiptItemEvent = new EventEmitter<ItemGuid>();

  removeReceipt(removeData: ItemGuid)
  {
    this.removeReceiptItemEvent.emit({ ...removeData })
  }
}
