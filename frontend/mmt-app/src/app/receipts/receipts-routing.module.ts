import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReceiptsComponent } from './receipts.component';
import { receiptsResolver } from './services/receipts.resolver';
import { ReceiptAddComponent } from './receipt-add/receipt-add.component';
import { ReceiptDetailsComponent } from './receipt-details/receipt-details.component';
import { receiptEntriesResolver } from './services/receipt-entries.resolver';
import { receiptItemResolver } from './services/receipt-item.resolver';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ReceiptsComponent,
    resolve: { itemsApi: receiptsResolver }
  },
  {
    path: 'add',
    pathMatch: 'full',
    component: ReceiptAddComponent,
  },
  {
    path: ':id',
    pathMatch: 'full',
    component: ReceiptDetailsComponent,
    resolve: { item: receiptItemResolver, itemsApi: receiptEntriesResolver }
  },

];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReceiptsRoutingModule { }
