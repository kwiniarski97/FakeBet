import {Injectable} from '@angular/core';
import {Headers, Http, RequestOptions, Response} from '@angular/http';
import {AppConfig} from '../app-config';
import {UserAuth} from '../models/userauth';
import {Service} from './service';
import 'rxjs/operator/map';
import {UserStatus} from '../models/userstatus';
import {User} from '../models/user';
import {ChangePasswordModel} from '../models/change-password-model';
import {LocalStorageService} from './localstorage.service';

@Injectable()
export class UserService extends Service {

  private serviceurl = this.config.apiUrl + '/user';

  constructor(private http: Http, private config: AppConfig, private localStorageService: LocalStorageService) {
    super();
  }

  getByNickname(nickName: string) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.get(this.serviceurl + '/get/' + nickName, jwt).map((response: Response) => response.json());
  }

  register(user: UserAuth) {
    return this.http.post(this.serviceurl + '/register', user);
  }

  changeStatus(nickName: string, status: UserStatus) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.put(this.serviceurl + '/changestatus/' + nickName, status, jwt);
  }

  top20() {
    this.http.get(this.serviceurl + '/top20').map((response: Response) => response.json());
  }

  updateEmail(user: User) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    this.http.post(this.serviceurl + '/updateemail', user, jwt);
  }


  deleteAccount(user: UserAuth) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.put(this.serviceurl + '/deleteaccount', user, jwt); // todo implement
  }

  changeEmail(user: any) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    return this.http.put(this.serviceurl + '/updateemail', user, jwt); // todo implement
  }

  changePassword(changePassword: ChangePasswordModel) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }

    const user = this.localStorageService.retrieve('currentUser');
    if (!user) {
      return;
    }
    changePassword.nickName = user.nickname;

    return this.http.put(this.serviceurl + '/changepassword', changePassword, jwt);
  }
}

