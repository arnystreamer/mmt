import { Component, OnInit } from '@angular/core';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'mmt-receipt-entry-details',
    templateUrl: './receipt-entry-details.component.html',
    styleUrls: [
        './receipt-entry-details.component.scss',
        '../../list-item-details.scss'
    ],
    standalone: false
})
export class ReceiptEntryDetailsComponent implements OnInit {
  public receiptEntry?: ReceiptEntry;

  constructor(private route: ActivatedRoute)
  {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ item }) => this.receiptEntry = item);
  }

}
