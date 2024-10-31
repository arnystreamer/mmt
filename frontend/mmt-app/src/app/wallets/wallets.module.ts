import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WalletsRoutingModule } from './wallets-routing.module';
import { WalletsComponent } from './wallets.component';
import { MatIconModule } from '@angular/material/icon';
import { FormControlsModule } from 'src/app/shared/form-controls.module';
import { WalletDetailsComponent } from './wallet-details/wallet-details.component';
import { WalletSectionDetailsComponent } from './wallet-section-details/wallet-section-details.component';



@NgModule({
  declarations: [
    WalletsComponent,
    WalletDetailsComponent,
    WalletSectionDetailsComponent
  ],
  imports: [
    CommonModule,
    WalletsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class WalletsModule { }
