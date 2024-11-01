import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Wallet } from '../models/wallet.model';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-wallets-list',
  templateUrl: './wallets-list.component.html',
  styleUrls: ['./wallets-list.component.scss']
})
export class WalletsListComponent {
  @Input() wallets!: Wallet[];
  @Output() createWalletItemEvent = new EventEmitter<Wallet>();
  @Output() removeWalletItemEvent = new EventEmitter<Wallet>();

  submitWallet(addData: ItemWithDescription)
  {
    this.createWalletItemEvent.emit({ userId: undefined, ...addData })
  }

  removeWallet(removeData: ItemWithDescription)
  {
    this.removeWalletItemEvent.emit({ userId: undefined, ...removeData })
  }
}
