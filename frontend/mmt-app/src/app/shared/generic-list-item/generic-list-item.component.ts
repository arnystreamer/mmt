import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-generic-list-item',
  templateUrl: './generic-list-item.component.html',
  styleUrls: [
    './generic-list-item.component.scss',
    '../../list-item-details.scss'
  ]
})
export class GenericListItemComponent {
  @Input() item?: ItemWithDescription;
  @Input() entityName: string | undefined;
  @Input() navigateRoute: any[] | undefined;
  @Output() removeEvent = new EventEmitter<ItemWithDescription>();

  constructor(private router: Router, private route: ActivatedRoute)
  {

  }

  detailsClick()
  {
    if (!this.item)
      return;

    if (this.navigateRoute)
    {
      this.router.navigate(this.navigateRoute, { relativeTo: this.route });
    }
  }

  deleteClick()
  {
    if (!this.item || !confirm(`Are you sure to delete ${this.entityName || ''} ${this.item.name}?`))
      return;

    this.removeEvent.emit(this.item);
  }
}
