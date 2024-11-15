import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSection, WalletSectionEdit } from '../../models/wallet-section.model';
import { ItemWithDescription } from 'src/app/models/item-with-description';
import { ItemIdentity } from 'src/app/models/item-identity';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';

@Component({
  selector: 'mmt-wallet-sections-list',
  templateUrl: './wallet-sections-list.component.html',
  styleUrls: ['./wallet-sections-list.component.scss']
})
export class WalletSectionsListComponent {
  @Input() sections!: WalletSection[];
  @Output() createSectionItemEvent = new EventEmitter<WalletSectionEdit>();
  @Output() removeSectionItemEvent = new EventEmitter<ItemIdentity>();

  submitWalletSection(addData: ItemWithDescriptionEdit)
  {
    this.createSectionItemEvent.emit({ ...addData })
  }

  removeWalletSection(removeData: ItemIdentity)
  {
    this.removeSectionItemEvent.emit({ ...removeData })
  }
}
