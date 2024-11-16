import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemGuid } from 'src/app/models/item-guid';
import { ProductEdit } from 'src/app/models/static-data/product-edit.model';
import { Product } from 'src/app/models/static-data/product.model';

@Component({
  selector: 'mmt-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.scss']
})
export class ProductsListComponent {
  @Input() items!: Product[];
  @Output() createProductEvent = new EventEmitter<ProductEdit>();
  @Output() removeProductEvent = new EventEmitter<ItemGuid>();

  submitProduct(addData: ProductEdit)
  {
    this.createProductEvent.emit({ ...addData })
  }

  removeProduct(removeData: ItemGuid)
  {
    this.removeProductEvent.emit({ ...removeData })
  }
}
