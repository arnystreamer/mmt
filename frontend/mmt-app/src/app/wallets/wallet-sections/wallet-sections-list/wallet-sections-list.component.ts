import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSection } from '../../models/wallet-section.model';
import { ItemIdentity } from 'src/app/models/item-identity';
import { ItemWithDescriptionEdit } from 'src/app/models/item-with-description-edit';
import { SectionEdit } from 'src/app/models/static-data/section-edit.model';

@Component({
    selector: 'mmt-wallet-sections-list',
    templateUrl: './wallet-sections-list.component.html',
    styleUrls: ['./wallet-sections-list.component.scss'],
    standalone: false
})
export class WalletSectionsListComponent {
  @Input() sections!: WalletSection[];
  @Output() createSectionItemEvent = new EventEmitter<SectionEdit>();
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
