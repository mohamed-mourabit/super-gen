import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { User$Component } from './user.component';


const routes: Routes = [
  { path: '', redirectTo: 'list', pathMatch: 'full'},
  { path: 'list', component: User$Component }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class User$RoutingModule { }
