import {Injectable} from '@angular/core';
import {Service} from './service';
import {Http} from '@angular/http';
import {AppConfig} from '../app-config';
import {Bet} from '../models/bet';

@Injectable()
export class BetService extends Service {

  private url = this.config.apiUrl + '/bet';

  constructor(private http: Http, private config: AppConfig) {
    super();
  }

  add(bet: Bet) {
    const jwt = BetService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    this.http.post(this.url + '/add', bet, jwt);
  }

}
