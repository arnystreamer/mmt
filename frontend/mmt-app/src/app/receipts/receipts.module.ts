import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceiptsRoutingModule } from './receipts-routing.module';
import { FormControlsModule } from '../shared/form-controls.module';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ReceiptsComponent } from './receipts.component';
import { ReceiptDetailsComponent } from './receipt-details/receipt-details.component';
import { ReceiptsListComponent } from './receipts-list/receipts-list.component';
import { ReceiptEntryDetailsComponent } from './receipt-entry-details/receipt-entry-details.component';
import { ReceiptEntriesListComponent } from './receipt-entries-list/receipt-entries-list.component';
import { ReceiptEntryListItemComponent } from './receipt-entry-list-item/receipt-entry-list-item.component';
import { ReceiptEntryAddComponent } from './receipt-entry-add/receipt-entry-add.component';
import { ReceiptListItemComponent } from './receipt-list-item/receipt-list-item.component';
import { ReceiptAddComponent } from './receipt-add/receipt-add.component';

@NgModule({
  declarations: [
    ReceiptsComponent,
    ReceiptDetailsComponent,
    ReceiptsListComponent,
    ReceiptEntryDetailsComponent,
    ReceiptEntriesListComponent,
    ReceiptEntryListItemComponent,
    ReceiptEntryAddComponent,
    ReceiptListItemComponent,
    ReceiptAddComponent
  ],
  imports: [
    CommonModule,
    ReceiptsRoutingModule,
    MatIconModule,
    FormControlsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatAutocompleteModule
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
  ]
})
export class ReceiptsModule { }
