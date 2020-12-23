
import { Injectable } from '@angular/core';
import { AccountService } from './account.service';
import configs from '../../assets/json/configs.json';
/*{imports}*/
@Injectable({
  providedIn: 'root'
})
export class UowService {
  configs = configs;
  accounts = new AccountService();
  /*{services}*/
  
  years = [...Array(new Date().getFullYear() - 2015).keys()].map(e => 2015 + e + 1);
  months = [...Array(12).keys()].map(e => e + 1);
  monthsAlpha = ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'].map((e, i) => ({ id: i + 1, name: e }));
  
  constructor() { }

  valideDate(date: Date): Date {
    date = new Date(date);

    const hoursDiff = date.getHours() - date.getTimezoneOffset() / 60;
    const minutesDiff = (date.getHours() - date.getTimezoneOffset()) % 60;
    date.setHours(hoursDiff);
    date.setMinutes(minutesDiff);

    return date;
  }

  arrayToString(t: string[]) {
    return t.map(e => `${e};`).reduce((p, c) => p + c);
  }

  stringToArray(s: string): string[] {
    const t = s.split(';');

    t.pop();

    return t;
  }
}
