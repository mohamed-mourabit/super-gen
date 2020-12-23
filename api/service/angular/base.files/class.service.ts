import { SuperService } from './super.service';
import { Injectable } from '@angular/core';
import { User$ } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class User$Service extends SuperService<User$> {

  constructor() {
    super('users');
  }

  getAll(startIndex, pageSize, sortBy, sortDir, /*{params}*/) {

    return this.http.get(`${this.urlApi}/${this.controller}/getAll/${startIndex}/${pageSize}/${sortBy}/${sortDir}/*{params2}*/`);
  }

  getAllForStatistique(/*{params}*/) {
    return this.http.get(`${this.urlApi}/${this.controller}/getAllForStatistique/*{params2}*/`);
  }

}
