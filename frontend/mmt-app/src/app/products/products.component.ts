import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models/static-data/product.model';
import { ProductsService } from './services/products.service';
import { ItemGuid } from '../models/item-guid';
import { ProductEdit } from '../models/static-data/product-edit.model';

@Component({
  selector: 'mmt-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  public items: Product[] = [];

  constructor(private route: ActivatedRoute,
    private productsService : ProductsService) {

  }

  ngOnInit(): void {
    this.route.data.subscribe(({ itemsApi }) => this.items.push(...itemsApi.items));
  }

  createProduct(item: ProductEdit)
  {
    this.productsService.post(item).subscribe({ next: v => { this.items.push(v) }});
  }

  removeProduct(item: ItemGuid)
  {
    this.productsService.delete(item.id).subscribe({ next: v => {
      if (v === false)
        return;

      const index = this.items.findIndex(i => i.id === item.id);
      this.items.splice(index, 1);
    }
    });
  }
}
