import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { GenericListItemComponent } from './generic-list-item/generic-list-item.component';
import { GenericInlineFormComponent } from './generic-inline-form/generic-inline-form.component';



@NgModule({
  declarations: [
    GenericListItemComponent,
    GenericInlineFormComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    GenericListItemComponent,
    GenericInlineFormComponent
  ]
})
export class FormControlsModule { }
