import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {LocalStorageService} from '../services/localstorage.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private localStorageService: LocalStorageService) {
  }

  canActivate(next: ActivatedRouteSnapshot,
              state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.localStorageService.isUserLogged()) {
      return true;
    } else {
      this.router.navigate(['/signin'], {queryParams: {returnUrl: state.url}});
      return false;
    }

  }
}
