import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatModule } from '../mat.module';
import { TableComponent } from './table/table.component';
import { FormComponent } from './form/form.component';
import { OneComponent } from './accueil/one/one.component';
import { AccueilComponent } from './accueil/accueil.component';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AdminComponent,
    TableComponent,
    FormComponent,
    AccueilComponent,
    OneComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatModule,
    ChartsModule,
  ]
})
export class AdminModule { }
