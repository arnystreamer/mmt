import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ItemGuid } from 'src/app/models/item-guid';
import { ProductEdit } from 'src/app/models/static-data/product-edit.model';
import { Product } from 'src/app/models/static-data/product.model';
import { Section } from 'src/app/models/static-data/section.model';

@Component({
    selector: 'mmt-products-list',
    templateUrl: './products-list.component.html',
    styleUrls: ['./products-list.component.scss'],
    standalone: false
})
export class ProductsListComponent {
  @Input() parent?: Product;
  @Input() items!: Product[];
  @Input() sections: Section[] = [];
  @Output() createProductEvent = new EventEmitter<ProductEdit>();
  @Output() removeProductEvent = new EventEmitter<ItemGuid>();

  getDirectChildItems(): Product[]
  {
    return this.items.filter(v => v.parentId === (this.parent?.id || null))
  }

  submitProduct(addData: ProductEdit)
  {
    this.createProductEvent.emit({ ...addData })
  }

  removeProduct(removeData: ItemGuid)
  {
    this.removeProductEvent.emit({ ...removeData })
  }
}
