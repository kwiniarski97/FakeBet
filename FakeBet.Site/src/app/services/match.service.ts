import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import {AppConfig} from '../app-config';
import {Service} from './service';
import {Match} from '../models/match';
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
    return this.http.post(this.url + '/add', match, jwt);
  }

  getNotStarted() {
    return this.http.get(this.url + '/getnotstarted').map((response: Response) => response.json());
  }

  getAll() {
    const jwt = MatchService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.get(this.url + '/getall', jwt);
  }

  delete(matchId: string) {
    const jwt = MatchService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.delete(this.url + `/delete/${matchId}`, jwt);
  }

  update(selectedMatch: Match) {
    const jwt = MatchService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    selectedMatch.matchTime = new Date(selectedMatch.matchTime.toDateString() + '0000');
    return this.http.put(this.url + '/update', selectedMatch, jwt);
  }

  end(matchId: string, winner: string) {
    const jwt = MatchService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.put(this.url + `/end/${matchId}`, {'winner': winner}, jwt);
  }
}
