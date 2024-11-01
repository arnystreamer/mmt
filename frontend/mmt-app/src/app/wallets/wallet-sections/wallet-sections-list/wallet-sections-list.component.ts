import { Component, EventEmitter, Input, Output } from '@angular/core';
import { WalletSection } from '../../wallet-section.model';

@Component({
  selector: 'mmt-wallet-sections-list',
  templateUrl: './wallet-sections-list.component.html',
  styleUrls: ['./wallet-sections-list.component.scss']
})
export class WalletSectionsListComponent {
  @Input() sections!: WalletSection[];
  @Output() createSectionItemEvent = new EventEmitter<WalletSection>();
  @Output() removeSectionItemEvent = new EventEmitter<WalletSection>();
}
