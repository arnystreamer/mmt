import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ReceiptEdit } from '../models/receipt-edit.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { WalletsService } from 'src/app/wallets/services/wallets.service';
import { LocationsService } from 'src/app/services/locations.service';
import { CurrenciesService } from 'src/app/services/currencies.service';
import { Location } from 'src/app/models/static-data/location.model';
import { Currency } from 'src/app/models/static-data/currency.model';
import { SharedAccount } from 'src/app/models/static-data/shared-account.model';
import { Wallet } from 'src/app/models/static-data/wallet.model';
import { ReceiptsService } from '../services/receipts.service';
import { ReceiptDefaultValues } from '../models/receipt-default-values.model';

@Component({
  selector: 'mmt-receipt-add',
  templateUrl: './receipt-add.component.html',
  styleUrls: [
    './receipt-add.component.scss',
    '../../forms.scss'
  ]
})
export class ReceiptAddComponent implements OnInit {
  @Output() createEvent = new EventEmitter<ReceiptEdit>();

  public form!: FormGroup;
  public isFormView: boolean = false;

  public isFormBusy = false;

  public wallets: Wallet[] = [];
  public sharedAccounts: SharedAccount[] = [];
  public locations: Location[] = [];
  public currencies: Currency[] = [];


  constructor(private fromBuilder: FormBuilder, private router: Router,
    private route: ActivatedRoute,
    private receiptsService : ReceiptsService,
    private walletsService: WalletsService,
    private locationsService: LocationsService,
    private currenciesService: CurrenciesService )
  {

  }

  ngOnInit(): void {

    this.walletsService.getAll(0, 10000).subscribe({next: w => this.wallets.push(...w.items) });
    this.locationsService.getAll(0, 10000).subscribe({next: l => this.locations.push(...l.items) });
    this.currenciesService.getAll(0, 10000).subscribe({next: c => this.currencies.push(...c.items) });

    const receiptDefaultValuesJson = localStorage.getItem('ReceiptDefaultValues');
    const receiptDefaultValues: ReceiptDefaultValues | undefined = receiptDefaultValuesJson ? JSON.parse(receiptDefaultValuesJson) : undefined;

    this.form = this.fromBuilder.group({
      date: [receiptDefaultValues?.date || new Date(), Validators.required],
      walletId: [receiptDefaultValues?.walletId || undefined],
      sharedAccountId: [receiptDefaultValues?.sharedAccountId || undefined],
      locationId: [receiptDefaultValues?.locationId || 0, Validators.min(1)],
      currencyId: [receiptDefaultValues?.currencyId || 0, Validators.min(1)],
      comment: [receiptDefaultValues?.comment || null]
    });
  }

  submitEntity()
  {
    if (this.form.valid)
    {
      this.receiptsService.post({ ...this.form.value}).subscribe({ next: v=>
        {
          this.isFormBusy = false;

          const receiptDefaultValues: ReceiptDefaultValues = {...this.form.value};
          localStorage.setItem('ReceiptDefaultValues', JSON.stringify(receiptDefaultValues));

          this.goToDetails(v.id);
        }
      });
    }
  }

  goBack()
  {
    this.router.navigate(['..'], { relativeTo: this.route });
  }

  goToDetails(id: string)
  {
    this.router.navigate(['../', id], { relativeTo: this.route });
  }
}
