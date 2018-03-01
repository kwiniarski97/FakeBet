import {Injectable} from '@angular/core';
import {Http, Response} from "@angular/http";
import {AppConfig} from "../app-config";
import 'rxjs/add/operator/map'
import {Observable} from "rxjs/Observable";

@Injectable()
export class AuthenticationService {
    
      
    constructor(private http: Http, private config: AppConfig) {
    }

    login(nickname: string, password: string):Observable<boolean> {
        return this.http.post(this.config.apiUrl + '/user/login', {
            nickname: nickname,
            password: password
        }).map((response: Response) => {
            let user = response.json();
            if (user && user.token) {
                localStorage.setItem("currentUser", JSON.stringify(user));
                return true;
            }
            return false;
            
        });
    }

    logout() {
        localStorage.removeItem('currentUser');
    }
}
