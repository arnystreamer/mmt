import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WalletsComponent } from './wallets.component';
import { walletsResolver } from './services/wallets.resolver';
import { walletItemResolver } from './services/wallet-item.resolver';
import { walletSectionsResolver } from './services/wallet-sections.resolver';
import { walletSectionItemResolver } from './services/wallet-section-item.resolver';
import { walletSectionCategoriesResolver } from './services/wallet-section-categories.resolver';
import { WalletDetailsComponent } from './wallet-details/wallet-details.component';
import { WalletSectionDetailsComponent } from './wallet-sections/wallet-section-details/wallet-section-details.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: WalletsComponent,
    resolve: { itemsApi: walletsResolver },
  },
  {
    path: ':id',
    component: WalletDetailsComponent,
    resolve: { item: walletItemResolver, itemsApi: walletSectionsResolver }
  },
  {
    path: ':id/sections/:sectionId',
    component: WalletSectionDetailsComponent,
    resolve: { parent: walletItemResolver, item: walletSectionItemResolver, itemsApi: walletSectionCategoriesResolver }
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WalletsRoutingModule { }
