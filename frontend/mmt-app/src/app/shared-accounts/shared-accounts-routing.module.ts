import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedAccountsComponent } from './shared-accounts.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: SharedAccountsComponent
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedAccountsRoutingModule { }
