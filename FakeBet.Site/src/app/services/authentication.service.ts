import {Injectable} from '@angular/core';
import {AppConfig} from '../app-config';
import 'rxjs/add/operator/map';
import {Observable} from 'rxjs/Observable';
import {Http, Response} from '@angular/http';
import {LocalStorageService} from './localstorage.service';
import {User} from '../models/user';

@Injectable()
export class AuthenticationService {


  constructor(private http: Http, private config: AppConfig, private localStorageService: LocalStorageService) {
  }

  login(nickname: string, password: string) {
    return this.http.post(this.config.apiUrl + '/user/login', {
      nickname: nickname,
      password: password
    });

  }

  logout() {
    this.localStorageService.delete('currentUser');
    this.localStorageService.delete('jwt');
  }
}
