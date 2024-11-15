import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ReceiptsComponent } from './receipts.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: ReceiptsComponent
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReceiptsRoutingModule { }
