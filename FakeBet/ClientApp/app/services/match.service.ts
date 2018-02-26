import {Injectable} from '@angular/core';
import {Http, Response} from "@angular/http";
import {AppConfig} from "../app-config";
import {Service} from "./service";
import {Match} from "../models/match";

@Injectable()
export class MatchService extends Service {

    private url = this.config.apiUrl + '/match';

    constructor(private http: Http, private config: AppConfig) {
        super();
    }

    get(matchid: string) {
        return this.http.get(this.url + `/get/${matchid}`).map((response: Response) => response.json());
    }

    add(match: Match) {
        let jwt = MatchService.getJwtHeaders();
        if (!jwt) {
            return;
        }
        this.http.post(this.url + '/add', match, jwt)
    }

    getNonStarted() {
        return this.http.get(this.url + '/getnonstarted').map((response: Response) => response.json());
    }

    changeStatus(match: Match, matchId: string) {
        let jwt = MatchService.getJwtHeaders();
        if (!jwt) {
            return;
        }
        this.http.put(this.url + `/changestatus/${matchId}`,match, jwt);
    }

}
