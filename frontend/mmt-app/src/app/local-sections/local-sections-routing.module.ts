import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LocalSectionsComponent } from './local-sections.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: LocalSectionsComponent
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LocalSectionsRoutingModule { }
