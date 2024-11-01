import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Wallet } from '../models/wallet.model';

@Component({
  selector: 'mmt-wallets-list',
  templateUrl: './wallets-list.component.html',
  styleUrls: ['./wallets-list.component.scss']
})
export class WalletsListComponent {
  @Input() wallets!: Wallet[];
  @Output() createWalletItemEvent = new EventEmitter<Wallet>();
  @Output() removeWalletItemEvent = new EventEmitter<Wallet>();
}
