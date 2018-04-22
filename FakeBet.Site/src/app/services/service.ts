import {Headers, RequestOptions} from '@angular/http';

export class Service {

  protected static getJwtHeaders() {
    const jwt = JSON.parse(localStorage.getItem('jwt'));
    if (!jwt) {
      return;
    }
    const headers = new Headers({'Authorization': 'Bearer ' + jwt});
    return new RequestOptions({headers: headers});
  }
}

