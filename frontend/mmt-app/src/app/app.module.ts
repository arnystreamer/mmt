import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';

import {MatCardModule} from '@angular/material/card';

import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { MainMenuComponent } from './main-menu/main-menu.component';
import { LayoutComponent } from './layout/layout.component';
import { StartPageComponent } from './start-page/start-page.component';
import { MatButtonModule } from '@angular/material/button';
import { ErrorComponent } from './error/error.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      {
        path: '',
        component: StartPageComponent
      },
      {
        path: 'admin',
        loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule)
      },
      {
        path: 'error',
        component: ErrorComponent
      },
      {
        path: '**',
        redirectTo: '/error'
      }

    ]
  }
]

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
    RouterModule.forRoot(routes),
    BrowserAnimationsModule,
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
