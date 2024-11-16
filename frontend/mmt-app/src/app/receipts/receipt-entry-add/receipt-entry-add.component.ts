import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ReceiptEntry } from '../models/receipt-entry.model';
import { ReceiptEntryEdit } from '../models/receipt-entry-edit.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsService } from 'src/app/products/services/products.service';
import { Product } from 'src/app/models/static-data/product.model';

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

  public form!: FormGroup;
  public isFormView: boolean = false;

  public products: Product[] = [];

  constructor(private fromBuilder: FormBuilder,
    private productsService: ProductsService
  )
  {

  }

  ngOnInit(): void {

    this.productsService.getAll(0, 10000).subscribe({next: p => this.products.push(...p.items) });

    this.form = this.fromBuilder.group({
      productId: [undefined, Validators.required],
      quantity: [0.0, Validators.min(0.00001)],
      price: [0.0, Validators.min(0.00001)]
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
    this.isFormView = false;
  }
}
