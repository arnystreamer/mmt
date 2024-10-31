import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedAccountsRoutingModule } from './shared-accounts-routing.module';
import { SharedAccountsComponent } from './shared-accounts.component';
import { FormControlsModule } from '../shared/form-controls.module';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    SharedAccountsComponent
  ],
  imports: [
    CommonModule,
    SharedAccountsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class SharedAccountsModule { }
