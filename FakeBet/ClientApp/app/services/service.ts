import {Headers, RequestOptions} from "@angular/http";

export class Service {

    protected static getJwtHeaders() {
        let storageItem = localStorage.getItem('currentUser');
        if (!storageItem) {
            return;
        }
        let currentUser = JSON.parse(storageItem);
        if (currentUser && currentUser.token) {
            let headers = new Headers({'Authorization': 'Bearer ' + currentUser.token});
            return new RequestOptions({headers: headers});
        }
    }
}
