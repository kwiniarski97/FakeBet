import {Injectable} from '@angular/core';
import {Service} from './service';
import {Http, Headers, RequestOptions, Response} from '@angular/http';
import {AppConfig} from '../app-config';
import {Bet} from '../models/bet';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class BetService extends Service {

  private url = this.config.apiUrl + '/bet';

  constructor(private http: Http, private config: AppConfig) {
    super();
  }

  addBet(bet: Bet) {
   const jwt = BetService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.post(this.url + '/add', bet, jwt);
  }
}
