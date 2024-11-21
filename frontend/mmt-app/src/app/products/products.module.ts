import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsRoutingModule } from './products-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { FormControlsModule } from '../shared/form-controls.module';
import { ProductsComponent } from './products.component';
import { ProductsListComponent } from './products-list/products-list.component';
import { ProductsListItemComponent } from './products-list-item/products-list-item.component';
import { ProductAddComponent } from './product-add/product-add.component';



@NgModule({
  declarations: [
    ProductsComponent,
    ProductsListComponent,
    ProductsListItemComponent,
    ProductAddComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    MatIconModule,
    MatSelectModule,
    FormControlsModule
  ]
})
export class ProductsModule { }
