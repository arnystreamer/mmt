import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemGuid } from 'src/app/models/item-guid';
import { Product } from 'src/app/models/static-data/product.model';

@Component({
  selector: 'mmt-products-list-item',
  templateUrl: './products-list-item.component.html',
  styleUrls: [
    './products-list-item.component.scss',
    '../../list-item-details.scss']
})
export class ProductsListItemComponent {
  @Input() item?: Product;
  @Output() removeEvent = new EventEmitter<ItemGuid>();

  constructor(private router: Router, private route: ActivatedRoute)
  {

  }

  detailsClick()
  {
    if (!this.item)
      return;

    this.router.navigate(['entries/',this.item.id], { relativeTo: this.route });
  }

  deleteClick()
  {
    if (!this.item || !confirm(`Are you sure to delete receipt entry ${this.item.id}?`))
      return;

    this.removeEvent.emit(this.item);
  }
}
