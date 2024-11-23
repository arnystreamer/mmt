import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ReceiptEntryEdit } from '../models/receipt-entry-edit.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsService } from 'src/app/products/services/products.service';
import { Product } from 'src/app/models/static-data/product.model';
import { Receipt } from '../models/receipt.model';

@Component({
  selector: 'mmt-receipt-entry-add',
  templateUrl: './receipt-entry-add.component.html',
  styleUrls: [
    './receipt-entry-add.component.scss',
    '../../list-item-details.scss',
    '../../forms.scss'
  ]
})
export class ReceiptEntryAddComponent implements OnInit {
  @Output() createEvent = new EventEmitter<ReceiptEntryEdit>();

  @Input() parentReceipt?: Receipt;

  public form!: FormGroup;
  public isFormView: boolean = false;

  public products: Product[] = [];

  constructor(private fromBuilder: FormBuilder,
    private productsService: ProductsService
  )
  {

  }

  ngOnInit(): void {

    this.productsService.getAll(0, 10000).subscribe({next: p =>
    {
      const walletId: number | undefined = this.parentReceipt?.walletId;
      const sharedAccountId: number | undefined = this.parentReceipt?.sharedAccountId;

      this.processIncomingProducts(null, 0, p.items);
    }});

    this.form = this.fromBuilder.group({
      productId: [undefined, Validators.required],
      quantity: [1.0, Validators.min(0.00001)],
      price: [0.0, Validators.min(0.00001)]
    });
  }

  processIncomingProducts(parentId: string | null, level: number, products: Product[])
  {
    products.filter(v => v.parentId === parentId).forEach((v) =>
    {
      const product = v;
      product.name = '>'.repeat(level) + ' ' + product.name;
      this.products.push(product);
      this.processIncomingProducts(product.id, level + 1, products);
    });
  }

  submitEntity()
  {
    if (this.form.valid)
    {
      this.createEvent.emit({...this.form.value});
      this.hideForm();
    }
  }

  showForm()
  {
    this.isFormView = true;
  }

  hideForm()
  {
    this.form.reset();
    this.form.controls['quantity'].setValue(1.0);
    this.form.controls['price'].setValue(0.0);
    this.isFormView = false;
  }
}
