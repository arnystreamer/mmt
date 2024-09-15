import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UsersModule } from './users/users.module';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

const routes: Routes = [
  {
    path: 'static-data',
    loadChildren: () => import('./static-data/static-data.module').then(m => m.StaticDataModule)
  },
  {
    path: 'users',
    loadChildren: () => import('./users/users.module').then(m => UsersModule)
  }
]

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MatInputModule,
    MatButtonModule
  ],
  exports: [ RouterModule ]
})
export class AdminModule { }
