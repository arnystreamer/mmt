import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Wallet } from '../models/wallet.model';
import { Router } from '@angular/router';

@Component({
  selector: 'mmt-wallets-list-item',
  templateUrl: './wallets-list-item.component.html',
  styleUrls: [
    './wallets-list-item.component.scss',
    '../../list-item-details.scss'
  ]
})
export class WalletsListItemComponent {
  @Input() wallet?: Wallet;
  @Output() removeEvent = new EventEmitter<Wallet>();

  constructor(private router: Router)
  {

  }

  detailsClick()
  {
    if (!this.wallet)
      return;

    this.router.navigate(['/wallets/', this.wallet.id])
  }

  deleteClick()
  {
    if (!this.wallet || !confirm(`Are you sure to delete ${this.wallet.name}?`))
      return;

    this.removeEvent.emit(this.wallet);
  }
}
