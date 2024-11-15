import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsRoutingModule } from './products-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { FormControlsModule } from '../shared/form-controls.module';
import { ProductsComponent } from './products.component';



@NgModule({
  declarations: [
    ProductsComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    MatIconModule,
    FormControlsModule
  ]
})
export class ProductsModule { }
