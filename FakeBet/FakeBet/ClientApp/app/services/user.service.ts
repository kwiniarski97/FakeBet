import {Injectable} from '@angular/core';
import {Headers, Http, RequestOptions, Response} from "@angular/http";
import {AppConfig} from "../app-config";
import {UserRegister, UserStatus} from "../models/user";
import {Service} from "./service";

@Injectable()
export class UserService extends Service {

    private serviceurl = this.config.apiUrl + '/user';

    constructor(private http: Http, private config: AppConfig) {
        super();
    }

    getByNickname(nickName: string) {
        let jwt = UserService.getJwtHeaders();
        if (!jwt) {
            return;
        }
        return this.http.get(this.serviceurl + '/get/' + nickName, jwt).map((response: Response) => response.json());
    }

    register(user: UserRegister) {
        return this.http.post(this.serviceurl + '/register', user);
    }

    changeStatus(nickName: string, status: UserStatus) {
        let jwt = UserService.getJwtHeaders();
        if (!jwt) {
            return;
        }
        return this.http.put(this.serviceurl + '/changestatus/' + nickName, status, jwt)
    }

    top20() {
        this.http.get(this.serviceurl + '/top20')
    }


}

