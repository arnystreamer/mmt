import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../models/static-data/product.model';
import { ProductsService } from './services/products.service';
import { ItemGuid } from '../models/item-guid';
import { ProductEdit } from '../models/static-data/product-edit.model';
import { SectionsService } from '../services/sections.service';
import { Section } from '../models/static-data/section.model';

@Component({
    selector: 'mmt-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.scss'],
    standalone: false
})
export class ProductsComponent implements OnInit {

  public items: Product[] = [];
  public sections: Section[] = [];

  constructor(private route: ActivatedRoute,
    private productsService : ProductsService,
    private sectionsService: SectionsService) {

  }

  ngOnInit(): void {
    this.sectionsService.getAll(0, 10000).subscribe({ next: v => this.sections = v.items });
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
