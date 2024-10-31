import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { StartPageComponent } from './start-page/start-page.component';
import { ErrorComponent } from './error/error.component';
import { authGuard } from './login/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'login',
        loadChildren: () => import('./login/login.module').then(m => m.LoginModule),
      },
      {
        path: '',
        component: LayoutComponent,
        children: [
          {
            path: '',
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
                path: 'wallets',
                loadChildren: () => import('./wallets/wallets.module').then(m => m.WalletsModule)
              },
              {
                path: 'shared-accounts',
                loadChildren: () => import('./shared-accounts/shared-accounts.module').then(m => m.SharedAccountsModule)
              },
              {
                path: 'local-sections',
                loadChildren: () => import('./local-sections/local-sections.module').then(m => m.LocalSectionsModule)
              },
            ],
            canActivateChild: [authGuard]
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
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
