import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { FormControlsModule } from '../shared/form-controls.module';
import { ServiceFunctionsComponent } from './service-functions/service-functions.component';
import { MatExpansionModule } from '@angular/material/expansion';


@NgModule({
  declarations: [
    LoginComponent,
    ServiceFunctionsComponent
  ],
  imports: [
    CommonModule,
    LoginRoutingModule,
    FormControlsModule,
    MatExpansionModule
  ]
})
export class LoginModule { }
