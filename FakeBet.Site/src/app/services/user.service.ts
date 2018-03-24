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

  deleteAccount(userAuth: UserAuth) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }

    const user = this.getCurrentUser();
    userAuth.nickname = user.nickName;

    return this.http.put(this.serviceurl + '/deleteaccount', userAuth, jwt); // todo implement

  }

  changeEmail(userAuth: UserAuth) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }
    const user = this.getCurrentUser();
    userAuth.nickname = user.nickName;

    return this.http.put(this.serviceurl + '/updateemail', userAuth, jwt); // todo implement
  }

  changePassword(changePassword: ChangePasswordModel) {
    const jwt = UserService.getJwtHeaders();
    if (!jwt) {
      return;
    }

    const user = this.getCurrentUser();
    changePassword.nickName = user.nickName;

    return this.http.put(this.serviceurl + '/changepassword', changePassword, jwt);
  }

  private getCurrentUser(): User {
    const user = this.localStorageService.retrieve('currentUser') as User;
    if (!user) {
      return;
    }
    return user;
  }
}

