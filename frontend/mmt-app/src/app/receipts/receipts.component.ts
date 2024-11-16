import { Component, OnInit } from '@angular/core';
import { ReceiptsService } from './services/receipts.service';
import { ActivatedRoute } from '@angular/router';
import { Receipt } from './models/receipt.model';
import { ReceiptEdit } from './models/receipt-edit.model';
import { ItemGuid } from '../models/item-guid';

@Component({
  selector: 'mmt-receipts',
  templateUrl: './receipts.component.html',
  styleUrls: ['./receipts.component.scss']
})
export class ReceiptsComponent implements OnInit {

  public items: Receipt[] = [];

  constructor(private route: ActivatedRoute,
    private receiptsService : ReceiptsService) {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  removeReceipt(item: ItemGuid)
  {
    this.receiptsService.delete(item.id).subscribe({ next: v => {
      if (v === false)
        return;

      const index = this.items.findIndex(i => i.id === item.id);
      this.items.splice(index, 1);
    }
    });
  }

}
