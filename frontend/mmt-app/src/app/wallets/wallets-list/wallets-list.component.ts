import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Wallet } from '../models/wallet.model';
import { WalletEdit } from '../models/wallet-edit.model';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';
import { ItemIdentity } from 'src/app/models/item-identity';

@Component({
  selector: 'mmt-wallets-list',
  templateUrl: './wallets-list.component.html',
  styleUrls: ['./wallets-list.component.scss']
})
export class WalletsListComponent {
  @Input() wallets!: Wallet[];
  @Output() createWalletItemEvent = new EventEmitter<WalletEdit>();
  @Output() removeWalletItemEvent = new EventEmitter<ItemIdentity>();

  submitWallet(addData: ItemWithDescriptionEdit)
  {
    this.createWalletItemEvent.emit({ ...addData })
  }

  removeWallet(removeData: ItemIdentity)
  {
    this.removeWalletItemEvent.emit({ ...removeData })
  }
}
