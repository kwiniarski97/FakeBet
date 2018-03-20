import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import {AppConfig} from '../app-config';
import {Service} from './service';
import {Match} from '../models/match';
import 'rxjs';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class MatchService extends Service {

  private url = this.config.apiUrl + '/match';

  constructor(private http: Http, private config: AppConfig) {
    super();
  }

  get(matchid: string): Observable<any> {
    return this.http.get(this.url + `/${matchid}`);
  }

  add(match: Match) {
    const jwt = MatchService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    this.http.post(this.url + '/add', match, jwt);
  }

  getNotStarted() {
    return this.http.get(this.url + '/getnotstarted').map((response: Response) => response.json());
  }

  changeStatus(match: Match, matchId: string) {
    const jwt = MatchService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    this.http.put(this.url + `/changestatus/${matchId}`, match, jwt);
  }
}
