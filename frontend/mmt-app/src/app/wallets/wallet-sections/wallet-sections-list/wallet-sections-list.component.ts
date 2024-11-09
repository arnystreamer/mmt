import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSection } from '../../models/wallet-section.model';
import { ItemWithDescription } from 'src/app/models/item-with-description';

@Component({
  selector: 'mmt-wallet-sections-list',
  templateUrl: './wallet-sections-list.component.html',
  styleUrls: ['./wallet-sections-list.component.scss']
})
export class WalletSectionsListComponent {
  @Input() sections!: WalletSection[];
  @Output() createSectionItemEvent = new EventEmitter<WalletSection>();
  @Output() removeSectionItemEvent = new EventEmitter<WalletSection>();

  submitWalletSection(addData: ItemWithDescription)
  {
    this.createSectionItemEvent.emit({ walletId: undefined, ...addData })
  }

  removeWalletSection(removeData: ItemWithDescription)
  {
    this.removeSectionItemEvent.emit({ walletId: undefined, ...removeData })
  }
}
