import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UsersAddComponent } from './users-add/users-add.component';
import { UsersListComponent } from './users-list/users-list.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserItemComponent } from './users-list/user-item/user-item.component';
import { UsersComponent } from './users.component';

const routes: Routes = [

];

@NgModule({
  declarations: [
    UsersAddComponent,
    UsersListComponent,
    UserDetailsComponent,
    UserItemComponent,
    UsersComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [ RouterModule ]
})
export class UsersModule { }
