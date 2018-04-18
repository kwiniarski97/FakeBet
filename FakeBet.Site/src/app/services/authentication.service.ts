import {Injectable} from '@angular/core';
import {AppConfig} from '../app-config';
import 'rxjs/add/operator/map';
import {Observable} from 'rxjs/Observable';
import {Http, Response} from '@angular/http';
import {LocalStorageService} from './localstorage.service';

@Injectable()
export class AuthenticationService {


  constructor(private http: Http, private config: AppConfig, private localStorageService: LocalStorageService) {
  }
  
  login(nickname: string, password: string): Observable<boolean> {
    return this.http.post(this.config.apiUrl + '/user/login', {
      nickname: nickname,
      password: password
    }).map((response: Response) => {
      const user = response.json();
      if (user && user.token) {
        this.localStorageService.save('currentUser', user);
        return true;
      }
      return false;

    });
  }

  logout() {
    this.localStorageService.delete('currentUser');
  }
}
