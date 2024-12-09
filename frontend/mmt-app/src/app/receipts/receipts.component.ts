import { Component, OnInit } from '@angular/core';
import { ReceiptsService } from './services/receipts.service';
import { ActivatedRoute } from '@angular/router';
import { Receipt } from './models/receipt.model';
import { ItemGuid } from '../models/item-guid';
import { LocationsService } from '../services/locations.service';
import { CurrenciesService } from '../services/currencies.service';
import { Location } from '../models/static-data/location.model';
import { Currency } from '../models/static-data/currency.model';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ReceiptRequestParams } from './models/receipt-request-params.model';

@Component({
    selector: 'mmt-receipts',
    templateUrl: './receipts.component.html',
    styleUrls: ['./receipts.component.scss'],
    standalone: false
})
export class ReceiptsComponent implements OnInit {

  public filterForm!: FormGroup;

  public items: Receipt[] = [];
  public locations: Location[] = [];
  public currencies: Currency[] = [];

  public filtersPanelExpanded: boolean = false;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private receiptsService: ReceiptsService,
    private locationsService: LocationsService,
    private currenciesService: CurrenciesService) {

  }

  ngOnInit(): void {
    this.locationsService.getAll(0, 10000).subscribe({ next: v => this.locations = v.items });
    this.currenciesService.getAll(0, 10000).subscribe({ next: v => this.currencies = v.items });

    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));

    const receiptRequestParamsJson = localStorage.getItem('ReceiptRequestParams');
    const receiptRequestParams: ReceiptRequestParams | undefined = receiptRequestParamsJson ? JSON.parse(receiptRequestParamsJson) : undefined;

    this.filterForm = this.formBuilder.group({
      dateFrom: [receiptRequestParams?.dateFrom || undefined],
      dateTo: [receiptRequestParams?.dateTo || undefined],
      locationId: [receiptRequestParams?.locationId || undefined],
      currencyId: [receiptRequestParams?.currencyId || undefined],
      sumFrom: [receiptRequestParams?.sumFrom || undefined],
      sumTo: [receiptRequestParams?.sumTo || undefined],
      comment: [receiptRequestParams?.comment || undefined],
    })
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

  resetFilters()
  {
    this.filterForm.reset();
  }

  applyFilters()
  {
    if (this.filterForm.valid)
    {
      this.filterForm.disable();
      this.receiptsService.getAll(this.filterForm.value, 0, 10000)
        .subscribe({ next: v => {
          this.items = v.items;
          this.filterForm.enable();

          const ReceiptRequestParams: ReceiptRequestParams = {...this.filterForm.value};
          localStorage.setItem('ReceiptRequestParams', JSON.stringify(ReceiptRequestParams));
        }});
    }
  }

}
