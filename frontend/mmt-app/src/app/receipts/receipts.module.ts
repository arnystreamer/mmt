import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceiptsRoutingModule } from './receipts-routing.module';
import { FormControlsModule } from '../shared/form-controls.module';
import { MatIconModule } from '@angular/material/icon';
import { ReceiptsComponent } from './receipts.component';

@NgModule({
  declarations: [
    ReceiptsComponent
  ],
  imports: [
    CommonModule,
    ReceiptsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class ReceiptsModule { }
