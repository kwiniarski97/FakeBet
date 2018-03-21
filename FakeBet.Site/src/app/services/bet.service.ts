import {Injectable} from '@angular/core';
import {Service} from './service';
import {Http, Headers, RequestOptions, Response} from '@angular/http';
import {AppConfig} from '../app-config';
import {Bet} from '../models/bet';

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
    this.http.post(this.url + '/add', bet, jwt).subscribe();
    // todo fix musisz zrobic zeby bet byl wysylany w innej formie bardziej application/json
  }
}
