import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AlertService} from '../../services/alert.service';
import {AuthenticationService} from '../../services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
  providers: [AuthenticationService, AlertService]
})
export class SignInComponent implements OnInit {

  model: any = {};
  processing = false;
  returnUrl: string;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private authenticationService: AuthenticationService,
              private alertService: AlertService) {
  }

  ngOnInit() {
    this.authenticationService.logout();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    if (this.returnUrl === '/logout') {
      this.returnUrl = '/';
    }
  }

  login() {
    this.processing = true;
    this.authenticationService.login(this.model.nickname, this.model.password).subscribe(data => {
        this.router.navigate([this.returnUrl]);
      },
      error => {
        this.processing = false;
        this.alertService.emitError(error._body);
      });
  }

}
