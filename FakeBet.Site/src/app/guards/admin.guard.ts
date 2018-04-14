import {Injectable} from '@angular/core';
import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {LocalStorageService} from '../services/localstorage.service';
import {UserRoles} from '../models/userenums';
import {User} from '../models/user';

@Injectable()
export class AdminGuard implements CanActivate {


  constructor(private router: Router, private localStorageService: LocalStorageService) {
  }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    const user = this.localStorageService.retrieve('currentUser') as User;
    if (user && user.role === UserRoles.Admin) {
      return true;
    }
    this.router.navigate(['/login']);
    return false;
  }
}
