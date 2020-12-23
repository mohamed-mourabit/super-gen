import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminComponent } from './admin.component';
import { AccueilComponent } from './accueil/accueil.component';
import { TableComponent } from './table/table.component';
import { FormComponent } from './form/form.component';
/*{imports}*/

const routes: Routes = [
  { path: '', redirectTo: '', pathMatch: 'full' },
  {
    path: '', component: AdminComponent,
    children: [
      { path: '', redirectTo: 'accueil', pathMatch: 'full' },
      { path: 'accueil', component: AccueilComponent, data: { name: 'fonction' } },
      { path: 'table', component: TableComponent, data: { name: 'fonction' } },
      { path: 'form', component: FormComponent, data: { name: 'fonction' } },
      // { path: 'dash', loadChildren: () => import('./dash/dash.module').then(m => m.DashModule), data: {animation: 'dash'} },
      /*{routes}*/
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
