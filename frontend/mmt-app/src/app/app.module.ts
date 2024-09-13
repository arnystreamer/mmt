import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

import { MatCardModule } from '@angular/material/card';

import { AppComponent } from './app.component';
import { RouterModule } from '@angular/router';
import { MainMenuComponent } from './main-menu/main-menu.component';
import { LayoutComponent } from './layout/layout.component';
import { StartPageComponent } from './start-page/start-page.component';
import { MatButtonModule } from '@angular/material/button';
import { ErrorComponent } from './error/error.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';



@NgModule({
  declarations: [
    AppComponent,
    MainMenuComponent,
    LayoutComponent,
    StartPageComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppModule { }
