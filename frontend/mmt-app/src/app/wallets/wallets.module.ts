import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WalletsRoutingModule } from './wallets-routing.module';
import { WalletsComponent } from './wallets.component';
import { MatIconModule } from '@angular/material/icon';
import { FormControlsModule } from 'src/app/shared/form-controls.module';
import { WalletDetailsComponent } from './wallet-details/wallet-details.component';
import { WalletSectionDetailsComponent } from './wallet-sections/wallet-section-details/wallet-section-details.component';
import { WalletsListComponent } from './wallets-list/wallets-list.component';
import { WalletSectionsListComponent } from './wallet-sections/wallet-sections-list/wallet-sections-list.component';
import { WalletSectionCategoriesListComponent } from './wallet-sections/wallet-section-categories-list/wallet-section-categories-list.component';



@NgModule({
  declarations: [
    WalletsComponent,
    WalletDetailsComponent,
    WalletSectionDetailsComponent,
    WalletsListComponent,
    WalletSectionsListComponent,
    WalletSectionCategoriesListComponent
  ],
  imports: [
    CommonModule,
    WalletsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class WalletsModule { }
