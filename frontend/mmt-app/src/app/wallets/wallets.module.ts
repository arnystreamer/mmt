import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WalletsRoutingModule } from './wallets-routing.module';
import { WalletsComponent } from './wallets.component';
import { MatIconModule } from '@angular/material/icon';
import { FormControlsModule } from 'src/app/shared/form-controls.module';



@NgModule({
  declarations: [
    WalletsComponent
  ],
  imports: [
    CommonModule,
    WalletsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class WalletsModule { }
