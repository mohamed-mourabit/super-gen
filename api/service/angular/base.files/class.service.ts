import { SuperService } from './super.service';
import { Injectable } from '@angular/core';
import { User$ } from '../models';

@Injectable({
  providedIn: 'root'
})
export class User$Service extends SuperService<User$> {

  constructor() {
    super('users');
  }
  
}
