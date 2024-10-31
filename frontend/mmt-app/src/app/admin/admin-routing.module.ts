import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminMenuPageComponent } from './admin-menu-page/admin-menu-page.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: AdminMenuPageComponent
  },
  {
    path: 'static-data',
    loadChildren: () => import('./static-data/static-data.module').then(m => m.StaticDataModule)
  },
  {
    path: 'users',
    loadChildren: () => import('./users/users.module').then(m => m.UsersModule)
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
