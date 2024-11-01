import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSection } from '../../wallet-section.model';
import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'mmt-wallet-sections-list-item',
  templateUrl: './wallet-sections-list-item.component.html',
  styleUrls: ['./wallet-sections-list-item.component.scss', '../../../list-item-details.scss']
})
export class WalletSectionsListItemComponent {
  @Input() section?: WalletSection;
  @Output() removeEvent = new EventEmitter<WalletSection>();

  constructor(private router: Router, private route: ActivatedRoute)
  {

  }

  detailsClick()
  {
    if (!this.section)
      return;

    this.router.navigate(['sections/', this.section.id], { relativeTo: this.route })
  }

  deleteClick()
  {
    if (!this.section || !confirm(`Are you sure to delete ${this.section.name}?`))
      return;

    this.removeEvent.emit(this.section);
  }
}
