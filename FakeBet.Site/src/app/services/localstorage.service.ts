import {Injectable} from '@angular/core';
import {UserRoles, UserStatus} from '../models/userenums';
import {User} from '../models/user';

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

  getUserRole(): UserRoles {
    this.CheckLocalStorage();
    const user = this.retrieve('currentUser') as User;
    if (!user) {
      return null;
    }
    return user.role;
  }

  private CheckLocalStorage() {
    if (typeof window !== 'undefined') {
      return;
    } else {
      throw new Error('Local Storage not defined');
    }
  }


}
