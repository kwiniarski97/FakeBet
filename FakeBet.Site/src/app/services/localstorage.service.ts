import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class LocalStorageService {
  constructor() {
  }

  save(key: string, data: object) {
    this.CheckLocalStorage();
    localStorage.setItem(key, JSON.stringify(data));
  }

  retrieve(key: string): object {
    this.CheckLocalStorage();
    const data = localStorage.getItem(key);
    return JSON.parse(data);
  }

  delete(key: string) {
    this.CheckLocalStorage();
    localStorage.removeItem(key);
  }

  isUserLogged(): boolean {
    this.CheckLocalStorage();
    return localStorage.getItem('currentUser') !== null;
  }

  private CheckLocalStorage() {
    if (typeof window !== 'undefined') {
      return;
    } else {
      throw new Error('Local Storage not defined');
    }
  }


}
