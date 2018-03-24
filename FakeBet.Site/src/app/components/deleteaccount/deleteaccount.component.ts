import {Component, OnInit} from '@angular/core';
import {AlertService} from '../../services/alert.service';
import {UserService} from '../../services/user.service';
import {UserAuth} from '../../models/userauth';
import {Router} from '@angular/router';
import {LocalStorageService} from '../../services/localstorage.service';

@Component({
  selector: 'app-deleteaccount',
  templateUrl: './deleteaccount.component.html',
  styleUrls: ['./deleteaccount.component.css']
})
export class DeleteAccountComponent implements OnInit {

  model: any = {};

  processing = false;

  constructor(private alertService: AlertService, private userService: UserService, private router: Router,
              private localStorageService: LocalStorageService) {
  }

  ngOnInit() {
  }

  deleteAccount() {
    this.alertService.emitConfirm('Are you sure you want to delete your account? Operation can\'t be reversed.');

    const user = new UserAuth();
    user.password = this.model.password;

    this.userService.deleteAccount(user).subscribe(response => {
      this.alertService.emitOk('Goodbye :)');
      this.router.navigate(['/']);
      this.localStorageService.delete('currentUser');
    }, error => {
      this.alertService.emitError(error._body);
    });
  }

}
