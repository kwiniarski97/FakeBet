import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AlertService} from '../../services/alert.service';
import {AuthenticationService} from '../../services/authentication.service';
import {UserService} from '../../services/user.service';
import {LocalStorageService} from '../../services/localstorage.service';

@Component({
  selector: 'app-login',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
  providers: [AuthenticationService, AlertService]
})
export class SignInComponent implements OnInit {

  model: any = {};
  processing = false;

  constructor(private router: Router,
              private authenticationService: AuthenticationService,
              private alertService: AlertService,
              private userService: UserService,
              private localStorageService: LocalStorageService) {
  }

  ngOnInit() {
    this.authenticationService.logout();
  }

  login() {
    this.processing = true;
    this.authenticationService.login(this.model.nickname, this.model.password).subscribe(data => {
        this.localStorageService.save('jwt', data['_body']);
        this.userService.getByNickname(this.model.nickname).subscribe(user => {
          this.localStorageService.save('currentUser', user);
        });

        this.router.navigate(['/']);
      },
      error => {
        this.processing = false;
        this.alertService.emitError(error._body);
      });
  }

}
